using MassTransit;
using Flight.Api.Consumers;
using Common.Message.Queue;
using Flight.Api.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Flight.Api.Entities;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddCommonMessageQueueServices();

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb"), config =>
    {
        config.MigrationsHistoryTable("_migrations");
    });
});

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<BookFlightConsumer, BookFlightConsumerDefinition>();
    busConfigurator.AddConsumer<BookFlightRollbackConsumer, BookFlightRollbackConsumerDefinition>();

    busConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(
            new Uri(builder.Configuration.GetConnectionString("RabbitMq") ?? throw new ArgumentNullException("RabbitMq connection string is missing")),
            host =>
            {
                host.Username("guest");
                host.Password("guest");
            });

        cfg.UseInMemoryOutbox(context);

        cfg.ConfigureEndpoints(context);
    });
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    using IServiceScope scope = app.Services.CreateScope();
    AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.MapGet("/flight", async (
    AppDbContext dbContext,
    CancellationToken cancellationToken) =>
{
    FlightRegistration[] collection = await dbContext.FlightRegistration
        .OrderByDescending(o => o.CreatedOnUtc)
        .ToArrayAsync();

    return Results.Ok(collection);
})
.WithName("Get flight registration collection");

app.Run();

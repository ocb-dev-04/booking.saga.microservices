using MassTransit;
using Microsoft.EntityFrameworkCore;
using RentCar.Api.Consumers;
using RentCar.Api.DatabaseContext;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb"), config =>
    {
        config.MigrationsHistoryTable("_mmigrations");
    });
});

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<RentCarConsumer, RentCarConsumerDefinition>();

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

    using IServiceScope scope = app.Services.CreateScope();
    AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    context.Database.Migrate();
}

app.Run();

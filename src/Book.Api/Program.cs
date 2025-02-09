using MassTransit;
using Book.Api.Saga;
using Scalar.AspNetCore;
using Book.Api.Consumer;
using Book.Api.DatabaseContext;
using Common.Message.Queue.Commands;
using Microsoft.EntityFrameworkCore;
using Common.Message.Queue.Events;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb"), config =>
    {
        config.MigrationsHistoryTable("_migrations");
    });
});

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<BookingCompletedConsumer, BookingCompletedConsumerDefinition>();

    busConfigurator.AddSagaStateMachine<BookingSaga, BookingSagaData>()
        .EntityFrameworkRepository(r =>
        {
            r.ExistingDbContext<AppDbContext>();

            r.UsePostgres();
        });

    busConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(
            new Uri(builder.Configuration.GetConnectionString("RabbitMq")!),
            host =>
            {
                host.Username("guest");
                host.Password("guest");
            });

        cfg.UseInMemoryOutbox(context);

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddOpenApi();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();

    using IServiceScope scope = app.Services.CreateScope();
    AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.MapPost("/book", async (
    BookingDetails body,
    IBus bus,
    CancellationToken cancellationToken) =>
{
    await bus.Publish(
        new BookingInitialized(
            body.Email,
            body.HotelName,
            body.FlightFrom,
            body.FlightTo,
            body.FlightCode,
            body.CarPlateNumber),
        cancellationToken);

    return Results.Ok();
})
.WithName("Create a new book");

app.MapGet("/book/success-history", async (
    AppDbContext dbContext,
    CancellationToken cancellationToken) =>
{
    BookingSagaData[] collection = await dbContext.BookingSagaData
        .Where(w => !w.SomeErrorOcurred)
        .OrderByDescending(o => o.SuccessOnUtc)
        .ToArrayAsync();

    return Results.Ok(collection);
})
.WithName("Get book success history")
.WithDescription("Return a saga sucess book collection");

app.MapGet("/book/failed-history", async (
    AppDbContext dbContext,
    CancellationToken cancellationToken) =>
{
    BookingSagaData[] collection = await dbContext.BookingSagaData
        .Where(w => w.SomeErrorOcurred)
        .OrderByDescending(o => o.FailedOnUtc)
        .ToArrayAsync();

    return Results.Ok(collection);
})
.WithName("Get book failed history")
.WithDescription("Return a saga failed book collection");

app.Run();

public sealed record BookingDetails(
    string Email,

    string HotelName,

    string FlightFrom,
    string FlightTo,
    string FlightCode,

    string CarPlateNumber);
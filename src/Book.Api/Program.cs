using MassTransit;
using Book.Api.Saga;
using Scalar.AspNetCore;
using Book.Api.DatabaseContext;
using Common.Message.Queue.Commands;
using Microsoft.EntityFrameworkCore;
using Book.Api.Consumer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb"), config =>
    {
        config.MigrationsHistoryTable("book_mmigrations");
    });
});

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<BookingCompletedConsumer, BookingCompletedConsumerDefinition>();

    busConfigurator.AddSagaStateMachine<BookingSaga, BookingSagaData>()
        .EntityFrameworkRepository(r =>
        {
            r.ExistingDbContext<BookDbContext>();

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
    BookDbContext context = scope.ServiceProvider.GetRequiredService<BookDbContext>();
    context.Database.EnsureCreated();
    context.Database.Migrate();
}

app.MapPost("/book", async (
    BookingDetails bookingDetails,
    IBus bus,
    CancellationToken cancellationToken) =>
{
    await bus.Publish(
        new BookHotelRequest(
            bookingDetails.Email, 
            bookingDetails.HotelName, 
            bookingDetails.FlightCode, 
            bookingDetails.CarPlateNumber),
        cancellationToken);

    return Results.Ok();
})
.WithName("Create a new book");

app.Run();

public class BookingDetails
{
    public string Email { get; set; } = string.Empty;
    public string HotelName { get; set; } = string.Empty;
    public string FlightCode { get; set; } = string.Empty;
    public string CarPlateNumber { get; set; } = string.Empty;
}
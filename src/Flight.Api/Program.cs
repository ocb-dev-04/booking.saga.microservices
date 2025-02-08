using Flight.Api.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<BookFlightConsumer, BookFlightConsumerDefinition>();

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
    app.MapOpenApi();

app.Run();

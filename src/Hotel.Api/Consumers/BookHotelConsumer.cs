using MassTransit;
using Hotel.Api.Entities;
using Hotel.Api.DatabaseContext;
using Common.Message.Queue.Events;
using Common.Message.Queue.Commands;
using Common.Message.Queue.Services;
using System.Text.Json;

namespace Hotel.Api.Consumers;

internal sealed class BookHotelConsumer(
    AppDbContext dbContext,
    IExceptionsHandlerService exceptionsHandlerService) : IConsumer<BookHotelRequest>
{
    public async Task Consume(ConsumeContext<BookHotelRequest> context)
    {
        await exceptionsHandlerService.ExecuteAsync(
            async () =>
            {
                Console.WriteLine($"Booking hotel {context.Message.HotelName} for traveler {context.Message.Email}");

                HotelRegistration created = new(
                    context.Message.Email,
                    context.Message.HotelName);
                await dbContext.HotelRegistration.AddAsync(created);
                await dbContext.SaveChangesAsync();

                await context.Publish(new HotelBooked(
                    context.Message.CorrelationId,
                    created.Id));
            },
            async ex => await context.Publish(new HotelBookedError(
                    context.Message.CorrelationId,
                    ex.Message.ToString(),
                    JsonSerializer.Serialize(ex.StackTrace))));
    }
}

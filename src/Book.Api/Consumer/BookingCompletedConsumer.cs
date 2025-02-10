using MassTransit;
using System.Text.Json;
using Common.Message.Queue.Events;
using Common.Message.Queue.Services;

namespace Book.Api.Consumer;

internal sealed class BookingCompletedConsumer(IExceptionsHandlerService exceptionsHandlerService) 
    : IConsumer<BookingCompleted>
{
    public async Task Consume(ConsumeContext<BookingCompleted> context)
    {
        await exceptionsHandlerService.ExecuteAsync(
            async () =>
            {
                // exception to test rollbac from here
                //throw new Exception($"Exception in {nameof(BookingCompletedConsumer)}");

                Console.WriteLine($"Booking process is completed for Traveler {context.Message.TravelerId}");

                await context.Publish(new BookingSucceed(context.Message.CorrelationId));
            },
            async ex =>
            {
                await context.Publish(new BookingCompletedError(
                    context.Message.CorrelationId,
                    ex.Message.ToString(),
                    JsonSerializer.Serialize(ex.StackTrace)));
            });
    }
}

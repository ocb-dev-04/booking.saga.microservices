using MassTransit;
using System.Text.Json;
using Common.Message.Queue.Events;
using Common.Message.Queue.Services;

namespace Book.Api.Consumer;

internal sealed class BookingCompletedConsumer(IExceptionsHandlerService exceptionsHandlerService) : IConsumer<BookingCompleted>
{
    public async Task Consume(ConsumeContext<BookingCompleted> context)
    {
        await exceptionsHandlerService.ExecuteAsync(
            async () =>
            {
                Console.WriteLine($"Booking process is completed for Traveler {context.Message.TravelerId}");
                await Task.CompletedTask;
            },
            ex =>
            {
                context.Publish(new BookingCompletedError(
                    context.Message.CorrelationId,
                    ex.Message.ToString(),
                    JsonSerializer.Serialize(ex.StackTrace)));
            });
    }
}

using MassTransit;
using System.Text.Json;
using Common.Message.Queue.Events;
using Common.Message.Queue.Services;

namespace Book.Api.Consumer;

internal sealed class BookingCompletedConsumer : IConsumer<BookingCompleted>
{
    public async Task Consume(ConsumeContext<BookingCompleted> context)
    {
        Console.WriteLine($"Booking process is completed for Traveler {context.Message.TravelerId}");
        await Task.CompletedTask;
    }
}

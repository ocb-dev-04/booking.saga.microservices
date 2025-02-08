using MassTransit;
using Common.Message.Queue.Events;

namespace Book.Api.Consumer;

internal sealed class BookingCompletedConsumer : IConsumer<BookingCompleted>
{
    public Task Consume(ConsumeContext<BookingCompleted> context)
    {
        Console.WriteLine($"Booking process is completed for Traveler {context.Message.TravelerId} with email {context.Message.Email}");

        return Task.CompletedTask;
    }
}

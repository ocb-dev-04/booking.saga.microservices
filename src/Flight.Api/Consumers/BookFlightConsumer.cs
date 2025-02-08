using MassTransit;
using Common.Message.Queue.Events;
using Common.Message.Queue.Commands;

namespace Flight.Api.Consumers;

internal sealed class BookFlightConsumer : IConsumer<BookFlightRequest>
{
    public async Task Consume(ConsumeContext<BookFlightRequest> context)
    {
        Console.WriteLine($"Booking flight number {context.Message.FlightCode} for traveler {context.Message.Email}");

        await context.Publish(new FlightBooked(
            context.Message.TravelerId,
            context.Message.Email,
            context.Message.FlightCode,
            context.Message.CarPlateNumber));
    }
}

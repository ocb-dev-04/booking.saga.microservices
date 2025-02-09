using MassTransit;
using Flight.Api.Entities;
using Flight.Api.DatabaseContext;
using Common.Message.Queue.Events;
using Common.Message.Queue.Commands;

namespace Flight.Api.Consumers;

internal sealed class BookFlightConsumer(
    AppDbContext dbContext) : IConsumer<BookFlightRequest>
{
    public async Task Consume(ConsumeContext<BookFlightRequest> context)
    {
        Console.WriteLine($"Booking flight number: {context.Message.FlightCode}. From: {context.Message.FlightFrom} - To: {context.Message.FlightTo}. Traveler id: {context.Message.TravelerId}");

        FlightRegistration created = new(
            context.Message.TravelerId, 
            context.Message.FlightCode, 
            context.Message.FlightFrom, 
            context.Message.FlightTo);

        await dbContext.FlightRegistration.AddAsync(created, context.CancellationToken);
        await dbContext.SaveChangesAsync(context.CancellationToken)
;
        await context.Publish(new FlightBooked(
            context.Message.CorrelationalId,
            context.Message.TravelerId,
            created.Id));
    }
}

using MassTransit;
using Flight.Api.Entities;
using Flight.Api.DatabaseContext;
using Common.Message.Queue.Events;
using Common.Message.Queue.Commands;
using Common.Message.Queue.Services;
using System.Text.Json;

namespace Flight.Api.Consumers;

internal sealed class BookFlightConsumer(
    AppDbContext dbContext,
    IExceptionsHandlerService exceptionsHandlerService) : IConsumer<BookFlightRequest>
{
    public async Task Consume(ConsumeContext<BookFlightRequest> context)
    {
        await exceptionsHandlerService.ExecuteAsync(
            async () =>
            {
                // exception to test rollbac from here
                //throw new Exception($"Exception in {nameof(BookFlightConsumer)}");

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
                    context.Message.CorrelationId,
                    context.Message.TravelerId,
                    created.Id));
            },
            async ex => await context.Publish(new FlightBookedError(
                    context.Message.CorrelationId,
                    ex.Message.ToString(),
                    JsonSerializer.Serialize(ex.StackTrace))));
    }
}

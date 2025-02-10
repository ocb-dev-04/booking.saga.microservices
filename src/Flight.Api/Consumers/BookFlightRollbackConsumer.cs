using MassTransit;
using Flight.Api.DatabaseContext;
using Common.Message.Queue.Events;
using Common.Message.Queue.Commands;
using Microsoft.EntityFrameworkCore;

namespace Flight.Api.Consumers;

internal sealed class BookFlightRollbackConsumer(AppDbContext dbContext) : IConsumer<BookFlightRollbackRequest>
{
    public async Task Consume(ConsumeContext<BookFlightRollbackRequest> context)
    {
        Console.WriteLine($"Rollback Booking flight with id: {context.Message.RegistrationId}");

        await dbContext.FlightRegistration
            .Where(w => w.Id.Equals(context.Message.RegistrationId))
            .ExecuteDeleteAsync(context.CancellationToken);
;
        await context.Publish(new FlightBookedError(
            context.Message.CorrelationId,
            string.Empty,
            string.Empty));
    }
}

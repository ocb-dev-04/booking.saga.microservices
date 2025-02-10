using MassTransit;
using Hotel.Api.DatabaseContext;
using Common.Message.Queue.Commands;
using Microsoft.EntityFrameworkCore;
using Common.Message.Queue.Events;

namespace Hotel.Api.Consumers;

internal sealed class BookHotelRollbackConsumer(AppDbContext dbContext) : IConsumer<BookHotelRollbackRequest>
{
    public async Task Consume(ConsumeContext<BookHotelRollbackRequest> context)
    {
        Console.WriteLine($"Rollback Booking hotel with id: {context.Message.RegistrationId}");

        await dbContext.HotelRegistration
            .Where(w => w.Id.Equals(context.Message.RegistrationId))
            .ExecuteDeleteAsync(context.CancellationToken);

        await context.Publish(new HotelBookedError(
            context.Message.CorrelationId,
            string.Empty,
            string.Empty));
    }
}

using MassTransit;
using Common.Message.Queue.Events;
using RentCar.Api.DatabaseContext;
using Common.Message.Queue.Commands;
using Microsoft.EntityFrameworkCore;

namespace RentCar.Api.Consumers;

internal sealed class RentCarRollbackConsumer(AppDbContext dbContext) 
    : IConsumer<RentCarRollbackRequest>
{
    public async Task Consume(ConsumeContext<RentCarRollbackRequest> context)
    {
        Console.WriteLine($"Rollback Car renting with id: {context.Message.RegistrationId}");
        
        await dbContext.CarRegistration
            .Where(w => w.Id.Equals(context.Message.RegistrationId))
            .ExecuteDeleteAsync(context.CancellationToken);

        await context.Publish(new CarRentedError(
            context.Message.CorrelationId,
            string.Empty,
            string.Empty));
    }
}

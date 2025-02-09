using MassTransit;
using RentCar.Api.Entities;
using Common.Message.Queue.Events;
using RentCar.Api.DatabaseContext;
using Common.Message.Queue.Commands;
using Common.Message.Queue.Services;
using System.Text.Json;

namespace RentCar.Api.Consumers;

internal sealed class RentCarConsumer(
    AppDbContext dbContext,
    IExceptionsHandlerService exceptionsHandlerService) : IConsumer<RentCarRequest>
{
    public async Task Consume(ConsumeContext<RentCarRequest> context)
    {
        await exceptionsHandlerService.ExecuteAsync(
            async () =>
            {
                throw new Exception("Testing rollback logic");

                Console.WriteLine($"Car renting - Plate Number {context.Message.CarPlateNumber} for traveler {context.Message.TravelerId}");

                CarRegistration created = new(
                    context.Message.TravelerId,
                    context.Message.CarPlateNumber);
                await dbContext.CarRegistration.AddAsync(created, context.CancellationToken);
                await dbContext.SaveChangesAsync(context.CancellationToken);

                await context.Publish(new CarRented(
                    context.Message.CorrelationId,
                    context.Message.TravelerId,
                    created.Id));
            },
            async ex => await context.Publish(new CarRentedError(
                    context.Message.CorrelationId, 
                    ex.Message.ToString(), 
                    JsonSerializer.Serialize(ex.StackTrace))));
    }
}

using MassTransit;
using RentCar.Api.Entities;
using Common.Message.Queue.Events;
using RentCar.Api.DatabaseContext;
using Common.Message.Queue.Commands;

namespace RentCar.Api.Consumers;

internal sealed class RentCarConsumer(AppDbContext dbContext) : IConsumer<RentCarRequest>
{
    public async Task Consume(ConsumeContext<RentCarRequest> context)
    {
        Console.WriteLine($"Car renting - Plate Number {context.Message.CarPlateNumber} for traveler {context.Message.TravelerId}");

        CarRegistration created = new(
            context.Message.TravelerId, 
            context.Message.CarPlateNumber);
        await dbContext.CarRegistration.AddAsync(created, context.CancellationToken);
        await dbContext.SaveChangesAsync(context.CancellationToken);

        await context.Publish(new CarRented(context.Message.TravelerId, created.Id));
    }
}

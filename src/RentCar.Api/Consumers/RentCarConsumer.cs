using MassTransit;
using Common.Message.Queue.Events;
using Common.Message.Queue.Commands;

namespace RentCar.Api.Consumers;

internal sealed class RentCarConsumer : IConsumer<RentCarRequest>
{
    public async Task Consume(ConsumeContext<RentCarRequest> context)
    {
        Console.WriteLine($"Car renting - Plate Number {context.Message.CarPlateNumber} for traveler {context.Message.Email}");

        await context.Publish(new CarRented(
            context.Message.TravelerId,
            context.Message.Email,
            context.Message.CarPlateNumber));
    }
}

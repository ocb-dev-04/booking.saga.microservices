using MassTransit;
using Hotel.Api.Entities;
using Hotel.Api.DatabaseContext;
using Common.Message.Queue.Events;
using Common.Message.Queue.Commands;

namespace Hotel.Api.Consumers;

internal sealed class BookHotelConsumer : IConsumer<BookHotelRequest>
{
    private readonly HotelDbContext _dbContext;

    public BookHotelConsumer(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<BookHotelRequest> context)
    {
        Console.WriteLine($"Booking hotel {context.Message.HotelName} for traveler {context.Message.Email}");

        Traveler traveler = new Traveler(Guid.NewGuid(), context.Message.Email, DateTime.Now);
        await _dbContext.Set<Traveler>().AddAsync(traveler);
        await _dbContext.SaveChangesAsync();

        await context.Publish(new HotelBooked(
            traveler.Id,
            traveler.Email,
            context.Message.HotelName,
            context.Message.FlightCode,
            context.Message.CarPlateNumber));
    }
}

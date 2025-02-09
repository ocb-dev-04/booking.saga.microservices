using MassTransit;
using Hotel.Api.Entities;
using Hotel.Api.DatabaseContext;
using Common.Message.Queue.Events;
using Common.Message.Queue.Commands;

namespace Hotel.Api.Consumers;

internal sealed class BookHotelConsumer : IConsumer<BookHotelRequest>
{
    private readonly AppDbContext _dbContext;

    public BookHotelConsumer(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<BookHotelRequest> context)
    {
        Console.WriteLine($"Booking hotel {context.Message.HotelName} for traveler {context.Message.Email}");

        HotelRegistration created = new (
            context.Message.Email, 
            context.Message.HotelName);
        await _dbContext.HotelRegistration.AddAsync(created);
        await _dbContext.SaveChangesAsync();

        await context.Publish(new HotelBooked(
            context.Message.CorrelationId, 
            created.Id));
    }
}

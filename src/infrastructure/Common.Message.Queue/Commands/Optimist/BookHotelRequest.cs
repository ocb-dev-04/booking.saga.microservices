using MassTransit;

namespace Common.Message.Queue.Commands;

[EntityName("saga-book-hotel-bind")]
public sealed record BookHotelRequest(
    Guid TravelerId,
    string Email, 
    string HotelName);
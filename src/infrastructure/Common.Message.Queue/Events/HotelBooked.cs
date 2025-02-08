using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("hotel-booked-saga-event")]
public sealed record HotelBooked(
    Guid TravelerId,
    string Email,
    string HotelName,
    string FlightCode,
    string CarPlateNumber);
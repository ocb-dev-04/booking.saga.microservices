using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("flight-booked-saga-event")]
public sealed record FlightBooked(
    Guid TravelerId,
    string Email,
    string FlightCode,
    string CarPlateNumber);
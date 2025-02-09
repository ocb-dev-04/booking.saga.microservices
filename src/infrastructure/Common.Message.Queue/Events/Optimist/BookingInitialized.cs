using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("saga-booking-initialized-event")]
public sealed record BookingInitialized(
    string Email,

    string HotelName,

    string FlightFrom,
    string FlightTo,
    string FlightCode,
    
    string CarPlateNumber)
{
    public Guid CorrelationId { get; init; } = Guid.NewGuid();
}

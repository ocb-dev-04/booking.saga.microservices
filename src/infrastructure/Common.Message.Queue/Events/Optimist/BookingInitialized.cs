using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("saga-booking-initialized-event")]
public sealed record BookingInitialized(
    Guid TravelerId,
    string Email,

    string HotelName,

    string FlightFrom,
    string FlightTo,
    string FlightCode,
    
    string CarPlateNumber);

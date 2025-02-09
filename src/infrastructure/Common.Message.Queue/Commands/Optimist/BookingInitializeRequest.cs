namespace Common.Message.Queue.Commands;

public sealed record BookingInitializeRequest(
    string Email,
    
    string HotelName,

    string FlightFrom,
    string FlightTo,
    string FlightCode,

    string CarPlateNumber);
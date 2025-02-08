using MassTransit;

namespace Common.Message.Queue.Commands;

[EntityName("book-flight-saga-bind")]
public sealed record BookFlightRequest(
    Guid TravelerId, 
    string Email, 
    string FlightCode, 
    string CarPlateNumber);
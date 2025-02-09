using MassTransit;

namespace Common.Message.Queue.Commands;

[EntityName("saga-book-flight-bind")]
public sealed record BookFlightRequest(
    Guid CorrelationId,
    Guid TravelerId, 
    string FlightFrom,
    string FlightTo,
    string FlightCode);
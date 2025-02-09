using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("saga-flight-booked-error-event")]
public sealed record FlightBookedError(
    Guid CorrelationId,
    string Message,
    string StackTrace);
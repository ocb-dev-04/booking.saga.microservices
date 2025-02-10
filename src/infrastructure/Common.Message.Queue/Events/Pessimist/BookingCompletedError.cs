using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("saga-booking-completed-error-event")]
public sealed record BookingCompletedError(
    Guid CorrelationId, 
    string Message,
    string StackTrace);

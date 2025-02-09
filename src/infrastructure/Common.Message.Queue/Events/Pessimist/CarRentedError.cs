using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("saga-car-rented-error-event")]
public sealed record CarRentedError(
    Guid CorrelationId,
    string Message, 
    string StackTrace);
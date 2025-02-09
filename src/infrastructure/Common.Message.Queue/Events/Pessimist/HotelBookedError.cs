using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("saga-hotel-booked-error-event")]
public sealed record HotelBookedError(
    Guid CorrelationId,
    Guid TravelerId,
    string Message,
    string StackTrace);
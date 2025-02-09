using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("saga-booking-completed-event")]
public sealed record BookingCompleted(Guid CorrelationId, Guid TravelerId);
using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("saga-booking-succeed-event")]
public sealed record BookingSucceed(Guid CorrelationId);
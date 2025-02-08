using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("booking-completed-saga-event")]
public sealed record BookingCompleted(
    Guid TravelerId,
    string Email);
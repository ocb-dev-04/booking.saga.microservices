using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("saga-hotel-booked-event")]
public sealed record HotelBooked(Guid CorrelationId, Guid TravelerId);
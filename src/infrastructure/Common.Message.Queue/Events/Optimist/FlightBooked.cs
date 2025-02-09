using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("saga-flight-booked-event")]
public sealed record FlightBooked(Guid CorrelationId, Guid TravelerId, Guid RegistrationId);
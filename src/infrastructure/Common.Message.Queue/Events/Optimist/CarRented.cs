using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("saga-car-rented-event")]
public sealed record CarRented(Guid CorrelationId, Guid TravelerId, Guid RegistrationId);
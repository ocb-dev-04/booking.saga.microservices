using MassTransit;

namespace Common.Message.Queue.Commands;

[EntityName("saga-rent-car-rollback-bind")]
public sealed record RentCarRollbackRequest(Guid CorrelationId, Guid RegistrationId);
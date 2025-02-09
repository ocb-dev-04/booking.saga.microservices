using MassTransit;

namespace Common.Message.Queue.Commands;

[EntityName("saga-book-flight-rollback-bind")]
public sealed record BookFlightRollbackRequest(Guid CorrelationId, Guid RegistrationId);
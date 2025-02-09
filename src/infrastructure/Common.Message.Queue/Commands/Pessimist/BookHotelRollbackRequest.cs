using MassTransit;

namespace Common.Message.Queue.Commands;

[EntityName("saga-book-hotel-rollback-bind")]
public sealed record BookHotelRollbackRequest(Guid CorrelationId, Guid RegistrationId);
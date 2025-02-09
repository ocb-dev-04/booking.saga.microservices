using MassTransit;

namespace Common.Message.Queue.Commands;

[EntityName("saga-rent-car-bind")]
public sealed record RentCarRequest(
    Guid CorrelationId,
    Guid TravelerId, 
    string CarPlateNumber);
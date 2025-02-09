using MassTransit;

namespace Common.Message.Queue.Commands;

[EntityName("saga-rent-car-bind")]
public sealed record RentCarRequest(
    Guid TravelerId, 
    string CarPlateNumber);
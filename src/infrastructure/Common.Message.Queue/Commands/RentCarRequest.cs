using MassTransit;

namespace Common.Message.Queue.Commands;

[EntityName("rent-car-saga-bind")]
public sealed record RentCarRequest(
    Guid TravelerId, 
    string Email, 
    string CarPlateNumber);
using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("car-rented-saga-event")]
public sealed record CarRented(
    Guid TravelerId,
    string Email,
    string CarPlateNumber);
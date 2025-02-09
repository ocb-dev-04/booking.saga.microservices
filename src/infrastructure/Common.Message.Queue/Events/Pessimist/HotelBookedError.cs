﻿using MassTransit;

namespace Common.Message.Queue.Events;

[EntityName("saga-hotel-booked-error-event")]
public sealed record HotelBookedError(
    Guid travelerId,
    string message,
    string stackTrace) : IEventError
{
    public Guid TravelerId => travelerId;
    public string Message => message;
    public string StackTrace => stackTrace;
}

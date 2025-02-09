﻿using MassTransit;

namespace Common.Message.Queue.Commands;

[EntityName("saga-book-flight-bind")]
public sealed record BookFlightRequest(
    Guid CorrelationalId,
    Guid TravelerId, 
    string FlightFrom,
    string FlightTo,
    string FlightCode);
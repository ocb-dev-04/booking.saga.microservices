using MassTransit;

namespace Common.Message.Queue.Commands;

[EntityName("book-hotel-saga-bind")]
public sealed record BookHotelRequest(
    string Email, 
    string HotelName, 
    string FlightCode, 
    string CarPlateNumber);
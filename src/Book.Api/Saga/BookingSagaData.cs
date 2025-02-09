using MassTransit;

namespace Book.Api.Saga;

public sealed class BookingSagaData : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    
    // step 1
    public Guid TravelerId { get; set; }
    public Guid HotelRegistrationId { get; set; }
    public string HotelName { get; set; } = string.Empty;
    public bool HotelBooked { get; set; }

    // step 2
    public Guid FlightRegistrationId { get; set; }
    public string FlightFrom { get; set; } = string.Empty;
    public string FlightTo { get; set; } = string.Empty;
    public string FlightCode { get; set; } = string.Empty;
    public bool FlightBooked { get; set; }

    // step 3
    public Guid CarRegistrationId { get; set; }
    public string CarPlateNumber { get; set; } = string.Empty;
    public bool CarRented { get; set; }

    // last step
    public bool BookingFinished { get; set; }
    public DateTimeOffset SuccessOnUtc { get; set; }

    /// if some error ocurreed
    public string ErrorMessage { get; set; } = string.Empty;
    public string StackTrace { get; set; } = string.Empty;
    public bool SomeErrorOcurred { get; set; } = false;
    public DateTimeOffset FailedOnUtc { get; set; }
}
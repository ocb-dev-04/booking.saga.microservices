namespace RentCar.Api.Entities;

public sealed class CarRegistration
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset CreatedOnUtc { get; init; } = DateTimeOffset.UtcNow;

    public Guid TravelerId { get; private set; }
    public string PlateNumber { get; private set; }

    public CarRegistration(
        Guid travelerId,
        string plateNumber)
    {
        TravelerId = travelerId;
        PlateNumber = plateNumber;
    }
}

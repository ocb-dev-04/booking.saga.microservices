namespace Flight.Api.Entities;

public sealed class FlightRegistration
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset CreatedOnUtc { get; init; } = DateTimeOffset.UtcNow;

    public Guid TravelerId { get; set; }
    public string Code { get; private set; }
    public string From { get; private set; }
    public string To { get; private set; }

    public FlightRegistration(
        Guid travelerId,
        string code,
        string from,
        string to)
    {
        TravelerId = travelerId;
        Code = code;
        From = from;
        To = to;
    }
}

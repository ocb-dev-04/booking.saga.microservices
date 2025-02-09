namespace Hotel.Api.Entities;

public sealed class HotelRegistration
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset CreatedOnUtc { get; init; } = DateTimeOffset.UtcNow;

    public string Email { get; private set; }
    public string HotelName { get; private set; }

    public HotelRegistration(string email, string hotelName)
    {
        Email = email;
        HotelName = hotelName;
    }
}

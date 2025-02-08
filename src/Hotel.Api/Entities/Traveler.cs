namespace Hotel.Api.Entities;

public sealed class Traveler
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime BookedOn { get; set; }

    public Traveler(Guid id, string email, DateTime bookedOn)
    {
        Id = id;
        Email = email;
        BookedOn = bookedOn.ToUniversalTime();
    }
}

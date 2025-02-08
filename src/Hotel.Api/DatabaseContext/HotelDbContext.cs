using Hotel.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Api.DatabaseContext;

internal sealed class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
    {
        
    }

    public DbSet<Traveler> Travelers { get; set; }

}

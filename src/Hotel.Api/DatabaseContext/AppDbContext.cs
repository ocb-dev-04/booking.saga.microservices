using Hotel.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Api.DatabaseContext;

internal sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<HotelRegistration> HotelRegistration { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<HotelRegistration> entityBuilder = modelBuilder.Entity<HotelRegistration>();

        entityBuilder.HasKey(k => k.Id);
        entityBuilder.HasIndex(s => new
        {
            s.Email,
            s.HotelName,
        });
    }
}

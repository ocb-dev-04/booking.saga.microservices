using RentCar.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentCar.Api.DatabaseContext;

internal sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<CarRegistration> CarRegistration { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<CarRegistration> entityBuilder = modelBuilder.Entity<CarRegistration>();

        entityBuilder.HasKey(k => k.Id);
        entityBuilder.HasIndex(s => new
        {
            s.TravelerId,
            s.PlateNumber,
        });
    }
}

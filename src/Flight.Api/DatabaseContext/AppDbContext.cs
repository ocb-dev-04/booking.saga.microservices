using Flight.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flight.Api.DatabaseContext;

internal sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<FlightRegistration> FlightRegistration { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<FlightRegistration> entityBuilder = modelBuilder.Entity<FlightRegistration>();

        entityBuilder.HasKey(k => k.Id);
        entityBuilder.HasIndex(s => new
        {
            s.Code,
            s.From,
            s.To
        });
    }
}

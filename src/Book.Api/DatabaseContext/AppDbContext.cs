using Book.Api.Saga;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.Api.DatabaseContext;

internal sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<BookingSagaData> BookingSagaData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<BookingSagaData> entityBuilder = modelBuilder.Entity<BookingSagaData>();

        entityBuilder.HasKey(k => k.CorrelationId);
        entityBuilder.HasIndex(s => new
        {
            s.CorrelationId,
            s.TravelerId,
            s.SuccessOnUtc,
            s.FailedOnUtc,
            s.SomeErrorOcurred
        });
    }
}

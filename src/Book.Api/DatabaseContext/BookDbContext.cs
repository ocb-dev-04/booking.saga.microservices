using Book.Api.Saga;
using Microsoft.EntityFrameworkCore;

namespace Book.Api.DatabaseContext;

public class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {

    }

    public DbSet<BookingSagaData> BookingSagaData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingSagaData>().HasKey(s => s.CorrelationId);
    }
}

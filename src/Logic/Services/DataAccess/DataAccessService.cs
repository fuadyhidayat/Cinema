using Logic.Movies.Constants;

namespace Logic.Services.DataAccess;

public class DataAccessService(DbContextOptions<DataAccessService> options) : DbContext(options)
{
    public DbSet<Movie> Movies => Set<Movie>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Movie>().ToTable("Movies");
        _ = modelBuilder.Entity<Movie>().Property(x => x.Title).HasColumnType($"nvarchar({MaximumLengthFor.Title})");
        _ = modelBuilder.Entity<Movie>().Property(x => x.Synopsis).HasColumnType($"nvarchar({MaximumLengthFor.Synopsis})");
        _ = modelBuilder.Entity<Movie>().Property(x => x.TicketPrice).HasColumnType("money");
    }
}

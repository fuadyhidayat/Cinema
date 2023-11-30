using MoviesMaximumLengthFor = Logic.Movies.Constants.MaximumLengthFor;
using DocumentsMaximumLengthFor = Logic.Documents.Constants.MaximumLengthFor;

namespace Logic.Services.DataAccess;

public class DataAccessService(DbContextOptions<DataAccessService> options) : DbContext(options)
{
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Document> Documents => Set<Document>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Movie>().ToTable("Movies");
        _ = modelBuilder.Entity<Movie>().Property(x => x.Title).HasColumnType($"nvarchar({MoviesMaximumLengthFor.Title})");
        _ = modelBuilder.Entity<Movie>().Property(x => x.Synopsis).HasColumnType($"nvarchar({MoviesMaximumLengthFor.Synopsis})");
        _ = modelBuilder.Entity<Movie>().Property(x => x.TicketPrice).HasColumnType("money");

        _ = modelBuilder.Entity<Document>().ToTable("Documents");
        _ = modelBuilder.Entity<Document>().Property(x => x.Title).HasColumnType($"nvarchar({DocumentsMaximumLengthFor.Title})");
        _ = modelBuilder.Entity<Document>().Property(x => x.FileName).HasColumnType($"nvarchar({DocumentsMaximumLengthFor.FileName})");
        _ = modelBuilder.Entity<Document>().Property(x => x.FileCode).HasColumnType($"nvarchar({DocumentsMaximumLengthFor.FileCode})");
    }
}

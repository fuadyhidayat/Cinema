using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.Services.DataAccess;

public class DataAccessService : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();
}

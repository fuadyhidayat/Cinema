using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logic.Services.DataAccess;

public static class ConfigureDataAccess
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DataAccess:ConnectionString"];

        services.AddDbContext<DataAccessService>(options =>
        {
            _ = options.UseSqlServer(connectionString, builder =>
            {
                _ = builder.MigrationsAssembly(typeof(DataAccessService).Assembly.FullName);
            });
        });

        return services;
    }
}

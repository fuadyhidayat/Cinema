using Microsoft.Extensions.DependencyInjection;

namespace Logic.Services.Storage;

public static class ConfigureStorage
{
    public static IServiceCollection AddStorage(this IServiceCollection services)
    {
        services.AddSingleton<StorageService>();

        return services;
    }
}

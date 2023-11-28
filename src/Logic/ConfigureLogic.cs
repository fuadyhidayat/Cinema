using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Logic;

public static class ConfigureLogic
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        _ = services.AddMediatR(configuration =>
        {
            _ = configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}

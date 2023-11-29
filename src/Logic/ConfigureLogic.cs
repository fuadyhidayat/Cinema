using System.Reflection;
using Logic.Common.Behaviours;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logic;

public static class ConfigureLogic
{
    public static IServiceCollection AddLogic(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        _ = services.AddMediatR(configuration =>
        {
            _ = configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            _ = configuration.AddOpenRequestPreProcessor(typeof(LoggingBehaviour<>));
            _ = configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddDataAccess(configuration);

        return services;
    }
}

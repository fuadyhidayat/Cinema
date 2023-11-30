namespace WebUI.Services.SimpleAuthorization;

public static class ConfigureSimpleAuthorization
{
    public static IServiceCollection AddSimpleAuthorization(this IServiceCollection services)
    {
        _ = services.AddScoped<SimpleAuthorizationService>();

        return services;
    }
}

using Logic.Services.CurrentUser;

namespace WebUI.Services.CurrentUser;

public static class ConfigureCurrentUser
{
    public static IServiceCollection AddCurrentUser(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}

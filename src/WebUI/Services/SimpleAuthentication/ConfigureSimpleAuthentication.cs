using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebUI.Services.SimpleAuthentication;

public static class ConfigureSimpleAuthentication
{
    public static IServiceCollection AddSimpleAuthentication(this IServiceCollection services)
    {
        services.AddScoped<SimpleAuthenticationService>();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

        return services;
    }
}

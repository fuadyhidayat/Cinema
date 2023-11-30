using Microsoft.Extensions.DependencyInjection;

namespace Logic.Services.Email;

public static class ConfigureEmail
{
    public static IServiceCollection AddEmail(this IServiceCollection services)
    {
        services.AddSingleton<EmailService>();

        return services;
    }
}

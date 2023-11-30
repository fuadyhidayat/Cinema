namespace WebUI.Services.SimpleUserProfile;

public static class ConfigureSimpleUserProfile
{
    public static IServiceCollection AddSimpleUserProfile(this IServiceCollection services)
    {
        services.AddScoped<SimpleUserProfileService>();

        return services;
    }
}

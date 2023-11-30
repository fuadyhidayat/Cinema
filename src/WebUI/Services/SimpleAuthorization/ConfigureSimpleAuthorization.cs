using Logic.Movies.Constants;

namespace WebUI.Services.SimpleAuthorization;

public static class ConfigureSimpleAuthorization
{
    public static IServiceCollection AddSimpleAuthorization(this IServiceCollection services)
    {
        _ = services.AddAuthorization(configure =>
        {
            configure.AddPolicy(PermissionFor.AddMovie, policy => policy.RequireClaim("Permission", PermissionFor.AddMovie));
            configure.AddPolicy(PermissionFor.EditMovie, policy => policy.RequireClaim("Permission", PermissionFor.EditMovie));
            configure.AddPolicy(PermissionFor.RemoveMovie, policy => policy.RequireClaim("Permission", PermissionFor.RemoveMovie));
        });

        _ = services.AddScoped<SimpleAuthorizationService>();

        return services;
    }
}

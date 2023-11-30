using MoviesPermissionFor = Logic.Movies.Constants.PermissionFor;
using DocumentsPermissionFor = Logic.Documents.Constants.PermissionFor;

namespace WebUI.Services.SimpleAuthorization;

public static class ConfigureSimpleAuthorization
{
    public static IServiceCollection AddSimpleAuthorization(this IServiceCollection services)
    {
        _ = services.AddAuthorization(configure =>
        {
            configure.AddPolicy(MoviesPermissionFor.AddMovie, policy => policy.RequireClaim("Permission", MoviesPermissionFor.AddMovie));
            configure.AddPolicy(MoviesPermissionFor.EditMovie, policy => policy.RequireClaim("Permission", MoviesPermissionFor.EditMovie));
            configure.AddPolicy(MoviesPermissionFor.RemoveMovie, policy => policy.RequireClaim("Permission", MoviesPermissionFor.RemoveMovie));

            configure.AddPolicy(DocumentsPermissionFor.AddDocument, policy => policy.RequireClaim("Permission", DocumentsPermissionFor.AddDocument));
            configure.AddPolicy(DocumentsPermissionFor.RemoveDocument, policy => policy.RequireClaim("Permission", DocumentsPermissionFor.RemoveDocument));
        });

        _ = services.AddScoped<SimpleAuthorizationService>();

        return services;
    }
}

using System.Security.Claims;

namespace Logic.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUsername(this ClaimsPrincipal principal)
    {
        var claimUsername = principal.FindFirst(x => x.Type == "Username");

        if (claimUsername is null)
        {
            return "anonymous";
        }

        return claimUsername.Value;
    }

    public static string GetName(this ClaimsPrincipal principal)
    {
        var claimName = principal.FindFirst(x => x.Type == "Name");

        if (claimName is null)
        {
            return "Anonymous";
        }

        return claimName.Value;
    }

    public static string GetEmail(this ClaimsPrincipal principal)
    {
        var claimEmail = principal.FindFirst(x => x.Type == "Email");

        if (claimEmail is null)
        {
            return "no-email@cinema.com";
        }

        return claimEmail.Value;
    }

    public static DateTimeOffset GetLoginTime(this ClaimsPrincipal user)
    {
        var claimLoginTime = user.FindFirst(x => x.Type == "LoginTime");

        if (claimLoginTime is null)
        {
            return new DateTimeOffset();
        }

        return DateTimeOffset.Parse(claimLoginTime.Value);
    }

    public static IEnumerable<string> GetRoles(this ClaimsPrincipal user)
    {
        return user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
    }

    public static IEnumerable<string> GetPermissions(this ClaimsPrincipal user)
    {
        return user.Claims.Where(x => x.Type == "Permission").Select(x => x.Value);
    }

    public static bool HasPermission(this ClaimsPrincipal user, string permission)
    {
        return user.Claims.Any(x => x.Type == "Permission" && x.Value == permission);
    }
}

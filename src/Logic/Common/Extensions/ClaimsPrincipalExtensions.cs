using System.Security.Claims;

namespace Logic.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUsername(this ClaimsPrincipal principal)
    {
        var claimName = principal.FindFirst(x => x.Type == "Username");

        if (claimName is null)
        {
            return "anonymous";
        }

        return claimName.Value;
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

    public static DateTimeOffset GetLoginTime(this ClaimsPrincipal user)
    {
        var claimLoginTime = user.FindFirst(x => x.Type == "LoginTime");

        if (claimLoginTime is null)
        {
            return new DateTimeOffset();
        }

        return DateTimeOffset.Parse(claimLoginTime.Value);
    }
}

using System.Security.Claims;
using System.Security;
using Logic.Services.CurrentUser;
using Logic.Common.Extensions;

namespace WebUI.Services.CurrentUser;

public class CurrentUserService : ICurrentUserService
{
    public string Username { get; private set; }
    public string Name { get; private set; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = new ClaimsPrincipal();

        if (httpContextAccessor.HttpContext is not null)
        {
            user = httpContextAccessor.HttpContext.User;
        }

        Username = user.GetUsername();
        Name = user.GetName();
    }
}

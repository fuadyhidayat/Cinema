using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebUI.Services.SimpleAuthentication;
using WebUI.Services.SimpleUserProfile;
using WebUI.Services.SimpleAuthorization;

namespace WebUI.Pages.Account;

public class LoginModel(
    SimpleAuthenticationService simpleAuthenticationService,
    SimpleUserProfileService simpleUserProfileService,
    SimpleAuthorizationService simpleAuthorizationService)
    : PageModel
{
    [BindProperty]
    public LoginViewModel Input { get; set; } = new LoginViewModel
    {
        Username = string.Empty,
        Password = string.Empty
    };

    public string ErrorMessage { get; set; } = string.Empty;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var userIsVerified = simpleAuthenticationService.VerifyCredentials(Input.Username, Input.Password);

        if (!userIsVerified)
        {
            ErrorMessage = "Invalid username or password.";

            return Page();
        }

        var userProfile = simpleUserProfileService.GetUserProfile(Input.Username);

        var claims = new List<Claim>
        {
            new("Username", Input.Username),
            new("Name", userProfile.Name),
            new("LoginTime", DateTimeOffset.Now.ToString())
        };

        var roles = simpleAuthorizationService.GetRoles(Input.Username);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        if (!string.IsNullOrEmpty(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return LocalRedirect("~/");
    }
}

public record LoginViewModel
{
    [Required]
    public required string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
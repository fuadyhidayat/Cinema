namespace WebUI.Services.SimpleAuthentication;

public record UserCredential
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}

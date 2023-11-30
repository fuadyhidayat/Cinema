namespace WebUI.Services.SimpleUserProfile;

public record UserProfile
{
    public required string Username { get; init; }
    public required string Name { get; init; }
}

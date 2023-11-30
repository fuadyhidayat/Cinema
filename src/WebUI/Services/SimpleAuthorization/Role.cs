namespace WebUI.Services.SimpleAuthorization;

public record Role
{
    public required string Name { get; init; }
    public required string[] Users { get; init; }
    public required string[] Permissions { get; init; }
}

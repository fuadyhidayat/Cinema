using System.Text.Json;

namespace WebUI.Services.SimpleUserProfile;

public class SimpleUserProfileService
{
    private static readonly JsonSerializerOptions _jsonSerializer = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public UserProfile GetUserProfile(string username)
    {
        var fileName = "UserProfiles.json";
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);
        var jsonContent = File.ReadAllText(filePath);

        using var jsonDocument = JsonDocument.Parse(jsonContent);
        var usersElement = jsonDocument.RootElement.EnumerateObject().First().Value;

        foreach (var userElement in usersElement.EnumerateArray())
        {
            var userProfile = userElement.Deserialize<UserProfile>(_jsonSerializer)
                ?? throw new Exception($"Failed to deserialize {fileName}.");

            if (userProfile.Username == username)
            {
                return userProfile;
            }
        }

        throw new Exception($"Failed to find user profile for {username}.");
    }
}

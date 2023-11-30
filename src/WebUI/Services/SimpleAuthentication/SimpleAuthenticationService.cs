using System.Text.Json;

namespace WebUI.Services.SimpleAuthentication;

public class SimpleAuthenticationService
{
    private static readonly JsonSerializerOptions _jsonSerializer = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public bool VerifyCredentials(string username, string password)
    {
        var fileName = "UserCredentials.json";
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);
        var jsonContent = File.ReadAllText(filePath);

        using var jsonDocument = JsonDocument.Parse(jsonContent);
        var usersElement = jsonDocument.RootElement.EnumerateObject().First().Value;

        foreach (var userElement in usersElement.EnumerateArray())
        {
            var userCredential = userElement.Deserialize<UserCredential>(_jsonSerializer)
                ?? throw new Exception($"Failed to deserialize {fileName}.");

            if (userCredential.Username == username && userCredential.Password == password)
            {
                return true;
            }
        }

        return false;
    }
}

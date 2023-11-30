using System.Text.Json;

namespace WebUI.Services.SimpleAuthorization;

public class SimpleAuthorizationService
{
    private static readonly JsonSerializerOptions _jsonSerializer = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public string[] GetRoles(string username)
    {
        var fileName = "Roles.json";
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);

        var roles = new List<string>();
        var jsonContent = File.ReadAllText(filePath);

        using var jsonDocument = JsonDocument.Parse(jsonContent);
        var rolesElement = jsonDocument.RootElement.EnumerateObject().First().Value;

        foreach (var roleElement in rolesElement.EnumerateArray())
        {
            var role = roleElement.Deserialize<Role>(_jsonSerializer)
                ?? throw new Exception($"Failed to deserialize {fileName}.");

            if (role.Users.Any(x => x == username))
            {
                roles.Add(role.Name);
            }
        }

        return roles.ToArray();
    }

    public string[] GetPermissions(string username)
    {
        var fileName = "Roles.json";
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);

        var permissions = new List<string>();
        var jsonContent = File.ReadAllText(filePath);

        using var jsonDocument = JsonDocument.Parse(jsonContent);
        var rolesElement = jsonDocument.RootElement.EnumerateObject().First().Value;

        foreach (var roleElement in rolesElement.EnumerateArray())
        {
            var role = roleElement.Deserialize<Role>(_jsonSerializer)
                ?? throw new Exception($"Failed to deserialize {fileName}.");

            if (role.Users.Any(x => x == username))
            {
                foreach (var permission in role.Permissions)
                {
                    if (!permissions.Any(x => x == permission))
                    {
                        permissions.Add(permission);
                    }
                }
            }
        }

        return permissions.ToArray();
    }
}

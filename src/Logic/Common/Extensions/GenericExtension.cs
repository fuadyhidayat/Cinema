using System.Text.Json;

namespace Logic.Common.Extensions;

public static class GenericExtensions
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        WriteIndented = true
    };

    public static string ToPrettyJson<T>(this T data)
    {
        return JsonSerializer.Serialize(data, _jsonSerializerOptions);
    }
}

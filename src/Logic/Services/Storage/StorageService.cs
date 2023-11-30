using Microsoft.Extensions.Configuration;

namespace Logic.Services.Storage;

public class StorageService
{
    private readonly string _folderPath;

    public StorageService(IConfiguration configuration)
    {
        var folderPath = configuration["Storage:FolderPath"];

        if (string.IsNullOrWhiteSpace(folderPath))
        {
            throw new Exception("Storage folder path is not configured");
        }

        _folderPath = folderPath;
    }

    public async Task<string> CreateAsync(byte[] content)
    {
        var fileCode = $"{Guid.NewGuid()}--{Guid.NewGuid()}";
        var filePath = Path.Combine(_folderPath, fileCode);

        using var fileStream = File.Create(filePath);
        await fileStream.WriteAsync(content.AsMemory(0, content.Length));

        return fileCode;
    }

    public Task DeleteAsync(string fileCode)
    {
        var filePath = Path.Combine(_folderPath, fileCode);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return Task.CompletedTask;
    }

    public Task<byte[]> ReadAsync(string fileCode)
    {
        return File.ReadAllBytesAsync(Path.Combine(_folderPath, fileCode));
    }
}

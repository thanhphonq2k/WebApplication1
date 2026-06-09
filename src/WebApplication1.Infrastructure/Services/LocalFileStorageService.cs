using Microsoft.Extensions.Hosting;
using WebApplication1.Application.Interfaces.IServices;

namespace WebApplication1.Infrastructure.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly IHostEnvironment _environment;

    public LocalFileStorageService(IHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SavePhotoAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        var photosDirectory = Path.Combine(_environment.ContentRootPath, "Photos");
        Directory.CreateDirectory(photosDirectory);

        var physicalPath = Path.Combine(photosDirectory, fileName);
        await using var stream = new FileStream(physicalPath, FileMode.Create);
        await fileStream.CopyToAsync(stream, cancellationToken);

        return fileName;
    }
}

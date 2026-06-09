namespace WebApplication1.Application.Interfaces.IServices;

public interface IFileStorageService
{
    Task<string> SavePhotoAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default);
}

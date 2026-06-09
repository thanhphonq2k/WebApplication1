using WebApplication1.Application.Common;
using WebApplication1.Application.Dtos;

namespace WebApplication1.Application.Interfaces.IUseCases;

public interface IEmployeeUseCase
{
    Task<Result<IReadOnlyList<EmployeeDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<string>> CreateAsync(EmployeeDto dto, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateAsync(EmployeeDto dto, CancellationToken cancellationToken = default);
    Task<Result<string>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<string>> SavePhotoAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default);
}

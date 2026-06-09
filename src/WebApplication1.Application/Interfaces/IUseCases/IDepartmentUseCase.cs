using WebApplication1.Application.Common;
using WebApplication1.Application.Dtos;

namespace WebApplication1.Application.Interfaces.IUseCases;

public interface IDepartmentUseCase
{
    Task<Result<IReadOnlyList<DepartmentDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<string>> CreateAsync(DepartmentDto dto, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateAsync(DepartmentDto dto, CancellationToken cancellationToken = default);
    Task<Result<string>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}

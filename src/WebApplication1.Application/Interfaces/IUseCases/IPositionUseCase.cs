using WebApplication1.Application.Common;
using WebApplication1.Application.Dtos;

namespace WebApplication1.Application.Interfaces.IUseCases;

public interface IPositionUseCase
{
    Task<Result<IReadOnlyList<PositionDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<string>> CreateAsync(PositionDto dto, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateAsync(PositionDto dto, CancellationToken cancellationToken = default);
    Task<Result<string>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}

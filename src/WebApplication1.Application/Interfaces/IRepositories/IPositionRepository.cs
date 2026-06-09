using WebApplication1.Domain.Entities;

namespace WebApplication1.Application.Interfaces.IRepositories;

public interface IPositionRepository
{
    Task<IReadOnlyList<PositionEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PositionEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(PositionEntity position, CancellationToken cancellationToken = default);
    Task UpdateAsync(PositionEntity position, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

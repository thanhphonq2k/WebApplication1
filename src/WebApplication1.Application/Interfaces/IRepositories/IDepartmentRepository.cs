using WebApplication1.Domain.Entities;

namespace WebApplication1.Application.Interfaces.IRepositories;

public interface IDepartmentRepository
{
    Task<IReadOnlyList<DepartmentEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DepartmentEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(DepartmentEntity department, CancellationToken cancellationToken = default);
    Task UpdateAsync(DepartmentEntity department, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

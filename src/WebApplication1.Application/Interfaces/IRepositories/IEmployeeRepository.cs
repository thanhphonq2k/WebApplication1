using WebApplication1.Domain.Entities;

namespace WebApplication1.Application.Interfaces.IRepositories;

public interface IEmployeeRepository
{
    Task<IReadOnlyList<EmployeeEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EmployeeEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(EmployeeEntity employee, CancellationToken cancellationToken = default);
    Task UpdateAsync(EmployeeEntity employee, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

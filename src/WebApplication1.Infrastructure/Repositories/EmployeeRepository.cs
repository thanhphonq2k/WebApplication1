using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Domain.Entities;
using WebApplication1.Infrastructure.Persistence;

namespace WebApplication1.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<EmployeeEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .AsNoTracking()
            .OrderBy(e => e.EmployeeId)
            .ToListAsync(cancellationToken);
    }

    public async Task<EmployeeEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id, cancellationToken);
    }

    public async Task AddAsync(EmployeeEntity employee, CancellationToken cancellationToken = default)
    {
        await _context.Employees.AddAsync(employee, cancellationToken);
    }

    public Task UpdateAsync(EmployeeEntity employee, CancellationToken cancellationToken = default)
    {
        _context.Employees.Update(employee);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id, cancellationToken);
        if (employee is not null)
            _context.Employees.Remove(employee);
    }
}

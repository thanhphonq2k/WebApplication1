using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Domain.Entities;
using WebApplication1.Infrastructure.Persistence;

namespace WebApplication1.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<DepartmentEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .OrderBy(d => d.DepartmentId)
            .ToListAsync(cancellationToken);
    }

    public async Task<DepartmentEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id, cancellationToken);
    }

    public async Task AddAsync(DepartmentEntity department, CancellationToken cancellationToken = default)
    {
        await _context.Departments.AddAsync(department, cancellationToken);
    }

    public Task UpdateAsync(DepartmentEntity department, CancellationToken cancellationToken = default)
    {
        _context.Departments.Update(department);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id, cancellationToken);
        if (department is not null)
            _context.Departments.Remove(department);
    }
}

using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Domain.Entities;
using WebApplication1.Infrastructure.Persistence;

namespace WebApplication1.Infrastructure.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly ApplicationDbContext _context;

    public PositionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<PositionEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Positions
            .AsNoTracking()
            .OrderBy(p => p.PositionId)
            .ToListAsync(cancellationToken);
    }

    public async Task<PositionEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Positions.FirstOrDefaultAsync(p => p.PositionId == id, cancellationToken);
    }

    public async Task AddAsync(PositionEntity position, CancellationToken cancellationToken = default)
    {
        await _context.Positions.AddAsync(position, cancellationToken);
    }

    public Task UpdateAsync(PositionEntity position, CancellationToken cancellationToken = default)
    {
        _context.Positions.Update(position);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var position = await _context.Positions.FirstOrDefaultAsync(p => p.PositionId == id, cancellationToken);
        if (position is not null)
            _context.Positions.Remove(position);
    }
}

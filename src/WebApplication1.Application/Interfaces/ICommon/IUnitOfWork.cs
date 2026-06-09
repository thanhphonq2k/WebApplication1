namespace WebApplication1.Application.Interfaces.ICommon;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

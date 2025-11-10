namespace WinFormsERP.Core.Interfaces.Repositories;

/// <summary>
/// Unit of Work pattern interface for transaction management
/// </summary>
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}

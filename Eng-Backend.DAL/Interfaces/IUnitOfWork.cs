namespace Eng_Backend.DAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPermissionDal Permissions { get; }
    IRoleDal Roles { get; }
    IUserDal Users { get; }
    
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}

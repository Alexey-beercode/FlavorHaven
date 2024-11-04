using FlavorHaven.DAL.Repositories.Interfaces;

namespace FlavorHaven.DAL.UnitOfWork;

public interface IUnitOfWork:IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
}
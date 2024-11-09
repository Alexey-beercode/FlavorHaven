using FlavorHaven.Domain.Abstractions.Repositories;

namespace FlavorHaven.Domain.Abstractions.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
}
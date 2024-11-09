using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using FlavorHaven.Infrastructure.Configuration;

namespace FlavorHaven.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private bool _disposed;

    public IUserRepository Users => _userRepository;
    public IRoleRepository Roles => _roleRepository;
    
    public UnitOfWork(AppDbContext dbContext, IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }
    }
}
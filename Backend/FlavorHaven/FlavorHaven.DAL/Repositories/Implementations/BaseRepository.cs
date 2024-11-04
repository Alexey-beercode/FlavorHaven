using FlavorHaven.DAL.Configuration;
using FlavorHaven.DAL.Repositories.Interfaces;
using FlavorHaven.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.DAL.Repositories.Implementations;

public class BaseRepository<T> : IBaseRepository<T> where T: BaseEntity
{
    protected readonly AppDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().Where(e => !e.IsDeleted).ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
    
}
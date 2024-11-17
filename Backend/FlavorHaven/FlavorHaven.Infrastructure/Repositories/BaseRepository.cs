using System.Linq.Expressions;
using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Common;
using FlavorHaven.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet;

    protected BaseRepository(AppDbContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        var query = _dbSet.Where(e => e.Id == id && !e.IsDeleted);

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        var query = _dbSet.Where(e => !e.IsDeleted).AsNoTracking();

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync(cancellationToken);
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
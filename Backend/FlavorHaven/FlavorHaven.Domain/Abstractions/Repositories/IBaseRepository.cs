using System.Linq.Expressions;
using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
}
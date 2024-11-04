using FlavorHaven.Domain.Common;

namespace FlavorHaven.DAL.Repositories.Interfaces;

public interface IBaseRepository<T> where T:BaseEntity
{
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
}
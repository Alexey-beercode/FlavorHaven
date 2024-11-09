using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface ICartRepository : IBaseRepository<Cart>
{
    Task<IEnumerable<Cart>> GetByUserId(Guid id, CancellationToken cancellationToken = default);
    Task DeleteByUserIdAsync(Guid id, CancellationToken cancellationToken = default);
}
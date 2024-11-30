using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<IEnumerable<Order>> GetByUserId(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetByStatusId(Guid id, CancellationToken cancellationToken = default);
    void Delete(Order order);
    Task<IEnumerable<Order>> GetAllWithIncludesAsync(CancellationToken cancellationToken = default);
}
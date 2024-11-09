using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IOrderStatusRepository : IBaseRepository<OrderStatus>
{
    Task<Role> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task DeleteAsync(OrderStatus status, CancellationToken cancellationToken = default);
}
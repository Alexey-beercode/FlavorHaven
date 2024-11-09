using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IOrderStatusRepository : IBaseRepository<OrderStatus>
{
    Task<OrderStatus> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    void Delete(OrderStatus status);
}
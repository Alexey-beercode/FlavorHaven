using System.Linq.Expressions;
using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IDishRepository : IBaseRepository<Dish>
{
    Task<IEnumerable<Dish>> Where(Expression<Func<Dish, bool>> filters, int pageNumber = 0, int pageSize = int.MaxValue,
        CancellationToken cancellationToken = default);
    void Delete(Dish dish);
}
using System.Linq.Expressions;
using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IDishRepository : IBaseRepository<Dish>
{
    Task<IEnumerable<Dish>> Where(Expression<Func<Dish, bool>> filters,
        CancellationToken cancellationToken = default, params Expression<Func<Dish, object>>[] includeProperties);
    
    Task DeleteAsync(Dish dish, CancellationToken cancellationToken = default);
}
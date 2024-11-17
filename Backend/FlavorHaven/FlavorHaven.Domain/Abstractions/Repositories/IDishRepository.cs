using System.Linq.Expressions;
using FlavorHaven.Domain.Entities;
using FlavorHaven.Domain.Enums;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IDishRepository : IBaseRepository<Dish>
{
    Task<IEnumerable<Dish>> Where(
        Expression<Func<Dish, bool>> filters, 
        SortingParameters? sorting = null,
        int pageNumber = 0, 
        int pageSize = int.MaxValue,
        CancellationToken cancellationToken = default);
    
    Task<int> Count(
        Expression<Func<Dish, bool>> filters,
        CancellationToken cancellationToken = default);
    
    void Delete(Dish dish);
}
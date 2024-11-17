using System.Linq.Expressions;
using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Entities;
using FlavorHaven.Domain.Enums;
using FlavorHaven.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.Infrastructure.Repositories;

public class DishRepository : BaseRepository<Dish>, IDishRepository
{
    private readonly AppDbContext _dbContext;
    
    public DishRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Dish>> Where(
        Expression<Func<Dish, bool>> filters, 
        SortingParameters? sorting = null,
        int pageNumber = 0, 
        int pageSize = int.MaxValue,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Dishes
            .Include(d => d.Category)
            .Where(filters);
        
        query = sorting switch
        {
            SortingParameters.PriceDecrease => query.OrderByDescending(d => d.Price),
            SortingParameters.PriceIncrease => query.OrderBy(d => d.Price),
            _ => query
        };
        
        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<int> Count(
        Expression<Func<Dish, bool>> filters,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Dishes
            .Where(filters)
            .CountAsync(cancellationToken);
    }

    public void Delete(Dish dish)
    {
        dish.IsDeleted = true;

        _dbContext.Dishes.Update(dish);
    }
}
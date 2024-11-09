using System.Linq.Expressions;
using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Entities;
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

    public async Task<IEnumerable<Dish>> Where(Expression<Func<Dish, bool>> filters, int pageNumber = 0, int pageSize = int.MaxValue,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Dishes.AsQueryable();
        
        query = query.Where(filters);
        
        query = query.Skip(pageNumber * pageSize).Take(pageSize);

        return await query.ToListAsync(cancellationToken);
    }

    public void Delete(Dish dish)
    {
        dish.IsDeleted = true;

        _dbContext.Dishes.Update(dish);
    }
}
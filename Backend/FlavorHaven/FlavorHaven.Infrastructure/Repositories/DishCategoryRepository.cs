using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Entities;
using FlavorHaven.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.Infrastructure.Repositories;

public class DishCategoryRepository : BaseRepository<DishCategory>, IDishCategoryRepository
{
    private readonly AppDbContext _dbContext;
    
    public DishCategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<DishCategory> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.DishCategories
            .FirstOrDefaultAsync(dc => dc.Name == name && !dc.IsDeleted, cancellationToken);
    }

    public void Delete(DishCategory dishCategory)
    {
        dishCategory.IsDeleted = true;

        _dbContext.DishCategories.Update(dishCategory);
    }
}
using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IDishCategoryRepository : IBaseRepository<DishCategory>
{
    Task<DishCategory> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    void Delete(DishCategory dishCategory);
}
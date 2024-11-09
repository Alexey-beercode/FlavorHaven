﻿using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IDishCategoryRepository : IBaseRepository<DishCategory>
{
    Task<Role> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task DeleteAsync(DishCategory dishCategory, CancellationToken cancellationToken = default);
}
using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.DishCategory.GetAllDishCategories;

public class GetAllDishCategoriesUseCase : IRequest<IEnumerable<DishCategoryDTO>>
{
    
}
using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.DishCategory.GetDishCategoriesById;

public class GetDishCategoriesByIdUseCase : IRequest<DishCategoryDTO>
{
    public Guid Id { get; set; }
}
using MediatR;

namespace FlavorHaven.Application.UseCases.DishCategory.CreateDishCategory;

public class CreateDishCategoryUseCase : IRequest
{
    public string Name { get; set; }
}
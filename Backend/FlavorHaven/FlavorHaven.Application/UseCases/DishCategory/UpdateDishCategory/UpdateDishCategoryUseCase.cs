using MediatR;

namespace FlavorHaven.Application.UseCases.DishCategory.UpdateDishCategory;

public class UpdateDishCategoryUseCase : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
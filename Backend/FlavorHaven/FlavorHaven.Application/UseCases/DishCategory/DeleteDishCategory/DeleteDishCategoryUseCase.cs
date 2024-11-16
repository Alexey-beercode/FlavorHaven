using MediatR;

namespace FlavorHaven.Application.UseCases.DishCategory.DeleteDishCategory;

public class DeleteDishCategoryUseCase : IRequest
{
    public Guid Id { get; set; }
}
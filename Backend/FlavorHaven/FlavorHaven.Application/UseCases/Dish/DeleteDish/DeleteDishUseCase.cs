using MediatR;

namespace FlavorHaven.Application.UseCases.Dish.DeleteDish;

public class DeleteDishUseCase : IRequest
{
    public Guid Id { get; set; }
}
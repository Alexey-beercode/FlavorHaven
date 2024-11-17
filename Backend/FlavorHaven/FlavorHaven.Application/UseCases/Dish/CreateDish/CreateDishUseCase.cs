using MediatR;

namespace FlavorHaven.Application.UseCases.Dish.CreateDish;

public class CreateDishUseCase : IRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public Guid CategoryId { get; set; }
}
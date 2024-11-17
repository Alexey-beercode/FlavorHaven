using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Dish.GetDishById;

public class GetDishByIdUseCase : IRequest<DishDTO>  
{
    public Guid Id { get; set; }
}
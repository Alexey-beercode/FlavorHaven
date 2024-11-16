using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Cart.RemoveFromCart;

public class RemoveFromCartUseCase : IRequest
{
    public Guid UserId { get; set; }
    public Guid DishId { get; set; }
    public int Count { get; set; }
}

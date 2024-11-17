using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Order.CreateOrderFromCart;

public class CreateOrderFromCartUseCase : IRequest
{
    public Guid UserId { get; set; }
    public string Address { get; set; }
    public string Note { get; set; }
}
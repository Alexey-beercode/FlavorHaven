using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Cart.GetCartsByUserId;

public class GetCartsByUserIdUseCase : IRequest<IEnumerable<CartDTO>>
{
    public Guid UserId { get; set; }
}
using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Order.GetOrdersByUserId;

public class GetOrdersByUserIdUseCase : IRequest<IEnumerable<OrderDTO>>
{
    public Guid UserId { get; set; }
}

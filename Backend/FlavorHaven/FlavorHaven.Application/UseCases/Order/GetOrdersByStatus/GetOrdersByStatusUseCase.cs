using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Order.GetOrdersByStatus;

public class GetOrdersByStatusUseCase : IRequest<IEnumerable<OrderDTO>>
{
    public Guid StatusId { get; set; }
}
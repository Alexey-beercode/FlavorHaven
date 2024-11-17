using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Order.GetOrderById;

public class GetOrderByIdUseCase : IRequest<OrderDTO>
{
    public Guid OrderId { get; set; }
}
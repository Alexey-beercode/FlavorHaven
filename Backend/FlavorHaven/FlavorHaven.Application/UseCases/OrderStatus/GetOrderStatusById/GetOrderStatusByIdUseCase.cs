using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.OrderStatus.GetOrderStatusById;

public class GetOrderStatusByIdUseCase : IRequest<OrderStatusDTO>
{
    public Guid Id { get; set; }
}
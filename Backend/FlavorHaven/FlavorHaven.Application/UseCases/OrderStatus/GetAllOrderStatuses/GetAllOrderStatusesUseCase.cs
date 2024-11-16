using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.OrderStatus.GetAllOrderStatuses;

public class GetAllOrderStatusesUseCase : IRequest<IEnumerable<OrderStatusDTO>>
{
    
}
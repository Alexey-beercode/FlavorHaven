using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Order.GetOrders;

public class GetOrdersUseCase:IRequest<IEnumerable<OrderDTO>>
{
    
}
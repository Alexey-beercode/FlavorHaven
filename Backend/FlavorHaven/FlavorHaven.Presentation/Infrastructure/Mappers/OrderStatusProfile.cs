using AutoMapper;
using FlavorHaven.Application.UseCases.OrderStatus.CreateOrderStatus;
using FlavorHaven.Application.UseCases.OrderStatus.UpdateOrderStatus;
using FlavorHaven.DTOs.OrderStatus;

namespace FlavorHaven.Infrastructure.Mappers;

public class OrderStatusProfile : Profile
{
    public OrderStatusProfile()
    {
        CreateMap<OrderStatusRequestDTO, CreateOrderStatusUseCase>();
        CreateMap<OrderStatusRequestDTO, UpdateOrderStatusUseCase>();
    }
}
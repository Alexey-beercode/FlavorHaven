using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.UseCases.OrderStatus.CreateOrderStatus;
using FlavorHaven.Application.UseCases.OrderStatus.UpdateOrderStatus;
using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Application.Infrastructure.Mappers;

public class OrderStatusProfile : Profile
{
    public OrderStatusProfile()
    {
        CreateMap<OrderStatus, OrderStatusDTO>();
        CreateMap<CreateOrderStatusUseCase, OrderStatus>();
        CreateMap<UpdateOrderStatusUseCase, OrderStatus>();
    }
}
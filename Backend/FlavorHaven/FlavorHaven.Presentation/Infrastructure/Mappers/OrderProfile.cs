using AutoMapper;
using FlavorHaven.Application.UseCases.Order.CreateOrderFromCart;
using FlavorHaven.Application.UseCases.Order.UpdateOrderStatus;
using FlavorHaven.DTOs.Order;

namespace FlavorHaven.Infrastructure.Mappers;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderRequestDTO, CreateOrderFromCartUseCase>();
        CreateMap<UpdateOrderRequestDTO, UpdateOrderStatusUseCase>();
    }
}
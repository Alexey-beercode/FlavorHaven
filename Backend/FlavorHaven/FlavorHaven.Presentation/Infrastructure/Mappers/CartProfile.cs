using AutoMapper;
using FlavorHaven.Application.UseCases.Cart.AddToCart;
using FlavorHaven.Application.UseCases.Cart.RemoveFromCart;
using FlavorHaven.DTOs.Cart;

namespace FlavorHaven.Infrastructure.Mappers;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CartItemRequestDTO, AddToCartUseCase>();
        CreateMap<CartItemRequestDTO, RemoveFromCartUseCase>();
    }
}
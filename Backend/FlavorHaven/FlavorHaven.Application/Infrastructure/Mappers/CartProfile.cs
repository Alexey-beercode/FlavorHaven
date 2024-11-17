using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.UseCases.Cart.AddToCart;
using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Application.Infrastructure.Mappers;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartDTO>();
    }
}

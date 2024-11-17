using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.UseCases.Dish.CreateDish;
using FlavorHaven.Application.UseCases.Dish.UpdateDish;
using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Application.Infrastructure.Mappers;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishDTO>();
        CreateMap<CreateDishUseCase, Dish>();
        CreateMap<UpdateDishUseCase, Dish>();
    }
}
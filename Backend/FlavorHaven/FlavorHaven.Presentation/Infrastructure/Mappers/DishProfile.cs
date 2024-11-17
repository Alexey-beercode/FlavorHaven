using AutoMapper;
using FlavorHaven.Application.UseCases.Dish.CreateDish;
using FlavorHaven.Application.UseCases.Dish.GetDishes;
using FlavorHaven.Application.UseCases.Dish.UpdateDish;
using FlavorHaven.Application.UseCases.DishCategory.CreateDishCategory;
using FlavorHaven.Application.UseCases.DishCategory.UpdateDishCategory;
using FlavorHaven.DTOs.Dish;
using FlavorHaven.DTOs.DishCategory;

namespace FlavorHaven.Infrastructure.Mappers;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<DishRequestDTO, CreateDishUseCase>();
        CreateMap<DishRequestDTO, UpdateDishUseCase>();
        CreateMap<GetDishesRequestDTO, GetDishesUseCase>();
    }
}
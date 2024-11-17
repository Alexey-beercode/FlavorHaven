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
        CreateMap<CreateDishRequestDTO, CreateDishUseCase>();
        CreateMap<UpdateDishRequestDTO, UpdateDishUseCase>();
        CreateMap<GetDishesRequestDTO, GetDishesUseCase>();

        CreateMap<DishCategoryRequestDTO, CreateDishCategoryUseCase>();
        CreateMap<DishCategoryRequestDTO, UpdateDishCategoryUseCase>();
    }
}
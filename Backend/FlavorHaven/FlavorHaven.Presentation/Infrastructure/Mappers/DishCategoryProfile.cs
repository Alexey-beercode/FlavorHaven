using AutoMapper;
using FlavorHaven.Application.UseCases.DishCategory.CreateDishCategory;
using FlavorHaven.Application.UseCases.DishCategory.UpdateDishCategory;
using FlavorHaven.DTOs.DishCategory;

namespace FlavorHaven.Infrastructure.Mappers;

public class DishCategoryProfile : Profile
{
    public DishCategoryProfile()
    {
        CreateMap<DishCategoryRequestDTO, CreateDishCategoryUseCase>();
        CreateMap<DishCategoryRequestDTO, UpdateDishCategoryUseCase>();
    }
}
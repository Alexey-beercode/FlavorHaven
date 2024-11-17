using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.UseCases.DishCategory.CreateDishCategory;
using FlavorHaven.Application.UseCases.DishCategory.UpdateDishCategory;
using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Application.Infrastructure.Mappers;

public class DishCategoryProfile : Profile
{
    public DishCategoryProfile()
    {
        CreateMap<DishCategory, DishCategoryDTO>();
        CreateMap<CreateDishCategoryUseCase, DishCategory>();
        CreateMap<UpdateDishCategoryUseCase, DishCategory>();
    }
}
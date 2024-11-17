using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.Models.Results;
using FlavorHaven.Domain.Enums;
using MediatR;

namespace FlavorHaven.Application.UseCases.Dish.GetDishes;

public class GetDishesUseCase : IRequest<PaginatedResult<DishDTO>>
{
    public Guid? CategoryId { get; set; }
    public string? SearchName{ get; set; }
    public SortingParameters? Sorting { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
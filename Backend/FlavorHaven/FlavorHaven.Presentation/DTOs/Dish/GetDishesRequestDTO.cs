using FlavorHaven.Domain.Enums;

namespace FlavorHaven.DTOs.Dish;

public class GetDishesRequestDTO
{
    public Guid? CategoryId { get; set; }
    public string? SearchName { get; set; }
    public SortingParameters? Sorting { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
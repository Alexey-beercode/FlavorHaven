namespace FlavorHaven.Application.Models.DTOs;

public class DishDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    
    public DishCategoryDTO Category { get; set; }
}
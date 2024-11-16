namespace FlavorHaven.Application.Models.DTOs;

public class CartDTO
{
    public Guid Id { get; set; }
    public int Count { get; set; }
    
    public DishDTO Dish { get; set; }
}
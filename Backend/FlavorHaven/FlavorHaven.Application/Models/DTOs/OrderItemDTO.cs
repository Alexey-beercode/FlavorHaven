namespace FlavorHaven.Application.Models.DTOs;

public class OrderItemDTO
{
    public Guid Id { get; set; }
    public int Count { get; set; }
    
    public DishDTO Dish { get; set; }
}
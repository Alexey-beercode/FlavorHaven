namespace FlavorHaven.DTOs.Dish;

public class DishRequestDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public Guid CategoryId { get; set; }
}
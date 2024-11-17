namespace FlavorHaven.DTOs.Cart;

public class CartItemRequestDTO
{
    public Guid DishId { get; set; }
    public int Count { get; set; }
}
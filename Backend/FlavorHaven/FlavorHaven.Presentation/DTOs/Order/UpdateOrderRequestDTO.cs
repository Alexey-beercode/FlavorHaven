namespace FlavorHaven.DTOs.Order;

public class UpdateOrderRequestDTO
{
    public Guid OrderId { get; set; }
    public Guid StatusId { get; set; }
}
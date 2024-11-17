namespace FlavorHaven.DTOs.Order;

public class UpdateOrderStatusRequestDTO
{
    public Guid OrderId { get; set; }
    public Guid StatusId { get; set; }
}
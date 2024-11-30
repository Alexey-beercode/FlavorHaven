namespace FlavorHaven.Application.Models.DTOs;

public class OrderDTO
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; }
    public DateTime CreatedTime { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public string Address { get; set; }
    public bool IsPayed { get; set; }
    public Guid UserId { get; set; }
    
    public OrderStatusDTO Status { get; set; }
    
    public ICollection<OrderItemDTO> OrderItems { get; set; }
}
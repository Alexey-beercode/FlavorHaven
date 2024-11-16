namespace FlavorHaven.Application.Models.DTOs;

public class PaymentDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid DishId { get; set; }
    public decimal Amount { get; set; }
    public bool IsCanceled { get; set; }
}
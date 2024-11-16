namespace FlavorHaven.Application.Models.DTOs;

public class ReviewDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
    public string Note { get; set; }
}
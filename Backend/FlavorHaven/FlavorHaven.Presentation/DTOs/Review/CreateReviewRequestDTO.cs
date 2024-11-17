namespace FlavorHaven.DTOs.Review;

public class CreateReviewRequestDTO
{
    public Guid OrderId { get; set; }
    public string Note { get; set; }
}
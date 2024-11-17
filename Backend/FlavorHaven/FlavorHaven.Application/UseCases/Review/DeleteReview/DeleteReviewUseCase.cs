using MediatR;

namespace FlavorHaven.Application.UseCases.Review.DeleteReview;

public class DeleteReviewUseCase : IRequest
{
    public Guid ReviewId { get; set; }
}

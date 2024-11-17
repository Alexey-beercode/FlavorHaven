using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Review.DeleteReview;

public class DeleteReviewUseCaseHandler : IRequestHandler<DeleteReviewUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReviewUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteReviewUseCase request, CancellationToken cancellationToken)
    {
        var review = await _unitOfWork.Reviews.GetByIdAsync(request.ReviewId, cancellationToken);
        if (review is null)
        {
            throw new EntityNotFoundException(nameof(Review), request.ReviewId);
        }

        _unitOfWork.Reviews.Delete(review);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
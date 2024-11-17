using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Review.GetReviewsByUserId;

public class GetReviewsByUserIdUseCase : IRequest<IEnumerable<ReviewDTO>>
{
    public Guid UserId { get; set; }
}
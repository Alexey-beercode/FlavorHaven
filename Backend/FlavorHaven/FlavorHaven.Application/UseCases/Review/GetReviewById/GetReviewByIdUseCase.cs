using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Review.GetReviewById;

public class GetReviewByIdUseCase : IRequest<ReviewDTO>
{
    public Guid ReviewId { get; set; }
}
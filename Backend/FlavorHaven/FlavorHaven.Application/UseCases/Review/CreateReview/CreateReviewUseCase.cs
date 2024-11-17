using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Review.CreateReview;

public class CreateReviewUseCase : IRequest
{
    public Guid OrderId { get; set; }
    public string Note { get; set; }
}
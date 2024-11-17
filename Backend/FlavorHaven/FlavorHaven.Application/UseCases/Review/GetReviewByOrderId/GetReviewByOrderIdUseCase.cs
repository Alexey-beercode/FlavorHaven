using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Review.GetReviewByOrderId;

public class GetReviewByOrderIdUseCase : IRequest<ReviewDTO>
{
    public Guid OrderId { get; set; }
}
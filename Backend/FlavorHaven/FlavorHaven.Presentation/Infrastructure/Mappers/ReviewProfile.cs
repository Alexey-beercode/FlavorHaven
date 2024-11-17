using AutoMapper;
using FlavorHaven.Application.UseCases.Review.CreateReview;
using FlavorHaven.DTOs.Review;

namespace FlavorHaven.Infrastructure.Mappers;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<CreateReviewRequestDTO, CreateReviewUseCase>();
    }
}
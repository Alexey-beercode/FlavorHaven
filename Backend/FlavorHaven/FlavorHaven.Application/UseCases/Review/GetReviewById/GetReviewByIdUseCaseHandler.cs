using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Review.GetReviewById;

public class GetReviewByIdUseCaseHandler : IRequestHandler<GetReviewByIdUseCase, ReviewDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetReviewByIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ReviewDTO> Handle(GetReviewByIdUseCase request, CancellationToken cancellationToken)
    {
        var review = await _unitOfWork.Reviews.GetByIdAsync(request.ReviewId, cancellationToken);
        if (review is null)
        {
            throw new EntityNotFoundException(nameof(Review), request.ReviewId);
        }

        return _mapper.Map<ReviewDTO>(review);
    }
}
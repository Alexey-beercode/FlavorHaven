using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Review.GetReviewByOrderId;

public class GetReviewByOrderIdUseCaseHandler : IRequestHandler<GetReviewByOrderIdUseCase, ReviewDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetReviewByOrderIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ReviewDTO> Handle(GetReviewByOrderIdUseCase request, CancellationToken cancellationToken)
    {
        var review = await _unitOfWork.Reviews.GetByOrderId(request.OrderId, cancellationToken);
        if (review is null)
        {
            throw new EntityNotFoundException("Review for order", request.OrderId);
        }

        return _mapper.Map<ReviewDTO>(review);
    }
}
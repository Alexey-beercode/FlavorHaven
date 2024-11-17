using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Review.GetReviewsByUserId;

public class GetReviewsByUserIdUseCaseHandler : IRequestHandler<GetReviewsByUserIdUseCase, IEnumerable<ReviewDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetReviewsByUserIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReviewDTO>> Handle(GetReviewsByUserIdUseCase request, CancellationToken cancellationToken)
    {
        var reviews = await _unitOfWork.Reviews.GetByUserId(request.UserId, cancellationToken);
        
        return _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
    }
}
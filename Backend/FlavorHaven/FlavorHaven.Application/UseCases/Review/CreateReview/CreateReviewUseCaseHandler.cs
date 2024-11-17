using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Review.CreateReview;

public class CreateReviewUseCaseHandler : IRequestHandler<CreateReviewUseCase>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateReviewUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(CreateReviewUseCase request, CancellationToken cancellationToken)
    {
        var existingReview = await _unitOfWork.Reviews.GetByOrderId(request.OrderId, cancellationToken);
        if (existingReview is not null)
        {
            throw new EntityAlreadyExistsException("Review for this order already exists");
        }

        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            throw new EntityNotFoundException(nameof(Order), request.OrderId);
        }

        var user = await _unitOfWork.Users.GetByIdAsync(order.UserId, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(User), order.UserId);
        }
        
        var review = new Domain.Entities.Review
        {
            OrderId = request.OrderId,
            UserId = order.UserId,
            Note = request.Note
        };

        await _unitOfWork.Reviews.CreateAsync(review, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
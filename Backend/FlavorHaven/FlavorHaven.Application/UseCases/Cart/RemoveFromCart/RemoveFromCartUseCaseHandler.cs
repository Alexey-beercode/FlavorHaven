using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Cart.RemoveFromCart;

public class RemoveFromCartUseCaseHandler : IRequestHandler<RemoveFromCartUseCase>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RemoveFromCartUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(RemoveFromCartUseCase request, CancellationToken cancellationToken)
    {
        var userCarts = await _unitOfWork.Cart.GetByUserId(request.UserId, cancellationToken);
        var existingCart = userCarts.FirstOrDefault(c => c.DishId == request.DishId);
        if (existingCart == null)
        {
            throw new EntityNotFoundException(nameof(Cart), $"UserId: {request.UserId}, DishId: {request.DishId}");
        }

        existingCart.Count -= request.Count;
        if (existingCart.Count <= 0)
        {
            _unitOfWork.Cart.Delete(existingCart);
        }
        else
        {
            _unitOfWork.Cart.Update(existingCart);
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Cart.AddToCart;

public class AddToCartUseCaseHandler : IRequestHandler<AddToCartUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddToCartUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddToCartUseCase request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.User), request.UserId);
        }

        var dish = await _unitOfWork.Dishes.GetByIdAsync(request.DishId, cancellationToken);
        if (dish is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Dish), request.DishId);
        }

        var userCarts = await _unitOfWork.Cart.GetByUserId(request.UserId, cancellationToken);
        
        var existingCart = userCarts.FirstOrDefault(c => c.DishId == request.DishId);
        if (existingCart is null)
        {
            var newCart = new Domain.Entities.Cart
            {
                UserId = request.UserId,
                DishId = request.DishId,
                Count = request.Count,
                Dish = dish
            };
            
            await _unitOfWork.Cart.CreateAsync(newCart, cancellationToken);
        }
        else
        {
            existingCart.Count += request.Count;
            _unitOfWork.Cart.Update(existingCart);
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
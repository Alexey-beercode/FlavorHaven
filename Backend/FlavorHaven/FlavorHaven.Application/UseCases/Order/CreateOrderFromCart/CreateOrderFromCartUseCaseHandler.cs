using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using FlavorHaven.Domain.Entities;
using MediatR;

namespace FlavorHaven.Application.UseCases.Order.CreateOrderFromCart;

public class CreateOrderFromCartUseCaseHandler : IRequestHandler<CreateOrderFromCartUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderFromCartUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateOrderFromCartUseCase request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.User), request.UserId);
        }

        var cartItems = await _unitOfWork.Cart.GetByUserId(request.UserId, cancellationToken);
        if (!cartItems.Any())
        {
            throw new EntityNotFoundException("Cart is empty");
        }
        
        var initialStatus = await _unitOfWork.OrderStatuses.GetByNameAsync("Processing", cancellationToken);
        if (initialStatus is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.OrderStatus), "Processing");
        }
        
        var order = new Domain.Entities.Order
        {
            OrderNumber = GenerateOrderNumber(),
            CreatedTime = DateTime.UtcNow,
            Amount = cartItems.Sum(i => i.Dish.Price * i.Count),
            Note = request.Note,
            Address = request.Address,
            IsPayed = false,
            UserId = request.UserId,
            StatusId = initialStatus.Id,
            OrderItems = cartItems
                .Select(i => new OrderItems
                {
                    Count = i.Count,
                    DishId = i.DishId
                }).ToList()
        };

        await _unitOfWork.Orders.CreateAsync(order, cancellationToken);
        
        await _unitOfWork.Cart.DeleteByUserIdAsync(request.UserId, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private string GenerateOrderNumber()
    {
        return DateTime.UtcNow.ToString("yyyyMMddHHmmss");
    }
}
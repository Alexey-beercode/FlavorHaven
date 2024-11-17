using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Payment.CreatePayment;

public class CreatePaymentUseCaseHandler : IRequestHandler<CreatePaymentUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePaymentUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreatePaymentUseCase request, CancellationToken cancellationToken)
    {
        var existingPayment = await _unitOfWork.Payments.GetByOrderId(request.OrderId, cancellationToken);
        if (existingPayment is not null)
        {
            throw new EntityNotFoundException("Payment for this order already exists");
        }

        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            throw new EntityNotFoundException(nameof(Order), request.OrderId);
        }

        var user = await _unitOfWork.Users.GetByIdAsync(order.UserId, cancellationToken);
        if (user == null)
        {
            throw new EntityNotFoundException(nameof(User), order.UserId);
        }

        var payment = new Domain.Entities.Payment
        {
            OrderId = request.OrderId,
            UserId = order.UserId,
            Amount = order.Amount,
            IsCanceled = false
        };

        if (user.Balance < order.Amount)
        {
            payment.IsCanceled = true;
            
            await _unitOfWork.Payments.CreateAsync(payment, cancellationToken);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            throw new InsufficientBalanceException(user.Id, user.Balance, order.Amount);
        }

        user.Balance -= order.Amount;
        _unitOfWork.Users.Update(user);

        await _unitOfWork.Payments.CreateAsync(payment, cancellationToken);
        
        order.IsPayed = true;
        _unitOfWork.Orders.Update(order);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
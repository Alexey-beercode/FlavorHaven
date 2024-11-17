using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Order.UpdateOrderStatus;

public class UpdateOrderStatusUseCaseHandler : IRequestHandler<UpdateOrderStatusUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderStatusUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateOrderStatusUseCase request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            throw new EntityNotFoundException(nameof(Order), request.OrderId);
        }

        var status = await _unitOfWork.OrderStatuses.GetByIdAsync(request.StatusId, cancellationToken);
        if (status is null)
        {
            throw new EntityNotFoundException(nameof(OrderStatus), request.StatusId);
        }

        order.StatusId = request.StatusId;

        _unitOfWork.Orders.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
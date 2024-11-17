using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Order.DeleteOrder;

public class DeleteOrderUseCaseHandler : IRequestHandler<DeleteOrderUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteOrderUseCase request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            throw new EntityNotFoundException(nameof(Order), request.OrderId);
        }

        _unitOfWork.Orders.Delete(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
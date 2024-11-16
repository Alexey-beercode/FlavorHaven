using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.OrderStatus.DeleteOrderStatus;

public class DeleteOrderStatusUseCaseHandler : IRequestHandler<DeleteOrderStatusUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderStatusUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteOrderStatusUseCase request, CancellationToken cancellationToken)
    {
        var status = await _unitOfWork.OrderStatuses.GetByIdAsync(request.Id, cancellationToken);
        if (status is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.OrderStatus), request.Id);
        }

        _unitOfWork.OrderStatuses.Delete(status);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
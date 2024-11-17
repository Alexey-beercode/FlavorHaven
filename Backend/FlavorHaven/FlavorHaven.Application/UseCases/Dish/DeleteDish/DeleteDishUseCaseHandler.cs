using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Dish.DeleteDish;

public class DeleteDishUseCaseHandler : IRequestHandler<DeleteDishUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDishUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteDishUseCase request, CancellationToken cancellationToken)
    {
        var dish = await _unitOfWork.Dishes.GetByIdAsync(request.Id, cancellationToken);
        if (dish is null)
        {
            throw new EntityNotFoundException(nameof(Dish), request.Id);
        }

        _unitOfWork.Dishes.Delete(dish);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
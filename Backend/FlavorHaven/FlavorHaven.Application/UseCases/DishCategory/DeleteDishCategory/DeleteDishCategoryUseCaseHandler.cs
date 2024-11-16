using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.DishCategory.DeleteDishCategory;

public class DeleteDishCategoryUseCaseHandler : IRequestHandler<DeleteDishCategoryUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDishCategoryUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteDishCategoryUseCase request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.DishCategories.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.DishCategory), request.Id);
        }

        _unitOfWork.DishCategories.Delete(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.DishCategory.UpdateDishCategory;

public class UpdateDishCategoryUseCaseHandler : IRequestHandler<UpdateDishCategoryUseCase>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateDishCategoryUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateDishCategoryUseCase request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.DishCategories.GetByNameAsync(request.Name, cancellationToken);
        if (category is not null)
        {
            throw new EntityAlreadyExistsException(nameof(Domain.Entities.DishCategory), request.Name);
        }

        category = await _unitOfWork.DishCategories.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.DishCategory), request.Id);
        }

        _mapper.Map(request, category);

        _unitOfWork.DishCategories.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
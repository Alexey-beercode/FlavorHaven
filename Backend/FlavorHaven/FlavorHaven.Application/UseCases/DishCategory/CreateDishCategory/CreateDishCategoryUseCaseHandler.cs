using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using FlavorHaven.Domain.Entities;
using MediatR;

namespace FlavorHaven.Application.UseCases.DishCategory.CreateDishCategory;

public class CreateDishCategoryUseCaseHandler : IRequestHandler<CreateDishCategoryUseCase>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDishCategoryUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(CreateDishCategoryUseCase request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.DishCategories.GetByNameAsync(request.Name, cancellationToken);
        if (category is not null)
        {
            throw new EntityAlreadyExistsException(nameof(Domain.Entities.DishCategory), request.Name);
        }

        category = _mapper.Map<Domain.Entities.DishCategory>(request);

        await _unitOfWork.DishCategories.CreateAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
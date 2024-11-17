using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Dish.CreateDish;

public class CreateDishUseCaseHandler : IRequestHandler<CreateDishUseCase>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDishUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(CreateDishUseCase request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.DishCategories.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category == null)
        {
            throw new EntityNotFoundException(nameof(DishCategory), request.CategoryId);
        }

        var dish = _mapper.Map<Domain.Entities.Dish>(request);

        await _unitOfWork.Dishes.CreateAsync(dish, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
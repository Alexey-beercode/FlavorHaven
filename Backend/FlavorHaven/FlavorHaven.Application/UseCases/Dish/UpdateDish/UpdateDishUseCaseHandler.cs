using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Dish.UpdateDish;

public class UpdateDishUseCaseHandler : IRequestHandler<UpdateDishUseCase>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateDishUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateDishUseCase request, CancellationToken cancellationToken)
    {
        var dish = await _unitOfWork.Dishes.GetByIdAsync(request.Id, cancellationToken);
        if (dish is null)
        {
            throw new EntityNotFoundException(nameof(Dish), request.Id);
        }

        var category = await _unitOfWork.DishCategories.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
        {
            throw new EntityNotFoundException(nameof(DishCategory), request.CategoryId);
        }

        _mapper.Map(request, dish);

        _unitOfWork.Dishes.Update(dish);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
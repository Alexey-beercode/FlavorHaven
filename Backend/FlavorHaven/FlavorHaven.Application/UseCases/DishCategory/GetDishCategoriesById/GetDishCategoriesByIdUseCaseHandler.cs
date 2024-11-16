using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.DishCategory.GetDishCategoriesById;

public class GetDishCategoriesByIdUseCaseHandler : IRequestHandler<GetDishCategoriesByIdUseCase, DishCategoryDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDishCategoriesByIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DishCategoryDTO> Handle(GetDishCategoriesByIdUseCase request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.DishCategories.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.DishCategory), request.Id);
        }

        return _mapper.Map<DishCategoryDTO>(category);
    }
}
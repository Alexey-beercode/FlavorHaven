using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.DishCategory.GetAllDishCategories;

public class GetAllDishCategoriesUseCaseHandler : IRequestHandler<GetAllDishCategoriesUseCase, IEnumerable<DishCategoryDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllDishCategoriesUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DishCategoryDTO>> Handle(GetAllDishCategoriesUseCase request, CancellationToken cancellationToken)
    {
        var categorise = await _unitOfWork.DishCategories.GetAllAsync(cancellationToken);

        return _mapper.Map<IEnumerable<DishCategoryDTO>>(categorise);
    }
}
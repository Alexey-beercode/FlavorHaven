using System.Linq.Expressions;
using AutoMapper;
using FlavorHaven.Application.Infrastructure.Extensions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.Models.Results;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using FlavorHaven.Domain.Enums;
using MediatR;

namespace FlavorHaven.Application.UseCases.Dish.GetDishes;

public class GetDishesUseCaseHandler : IRequestHandler<GetDishesUseCase, PaginatedResult<DishDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDishesUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<DishDTO>> Handle(GetDishesUseCase request, CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.Dish, bool>> filter = dish => !dish.IsDeleted;

        if (request.CategoryId.HasValue)
        {
            var categoryId = request.CategoryId.Value;
            filter = filter.And(dish => dish.CategoryId == categoryId);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchName))
        {
            var searchTerm = request.SearchName.ToLower();
            filter = filter.And(dish => dish.Name.ToLower().Contains(searchTerm));
        }

        var pageNumber = request.PageNumber ?? 0;
        var pageSize = request.PageSize ?? int.MaxValue;

        var totalCount = await _unitOfWork.Dishes.Count(filter, cancellationToken);
        
        var dishes = await _unitOfWork.Dishes.Where(
            filter,
            request.Sorting,
            pageNumber,
            pageSize,
            cancellationToken);

        return new PaginatedResult<DishDTO>
        {
            Collection = _mapper.Map<IEnumerable<DishDTO>>(dishes),
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalItemCount = totalCount,
            TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
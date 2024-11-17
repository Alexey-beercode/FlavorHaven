using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Dish.GetDishById;

public class GetDishByIdUseCaseHandler : IRequestHandler<GetDishByIdUseCase, DishDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDishByIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DishDTO> Handle(GetDishByIdUseCase request, CancellationToken cancellationToken)
    {
        var dish = await _unitOfWork.Dishes.GetByIdAsync(
            request.Id, 
            cancellationToken,
            d => d.Category);

        if (dish is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Dish), request.Id);
        }

        return _mapper.Map<DishDTO>(dish);
    }
}
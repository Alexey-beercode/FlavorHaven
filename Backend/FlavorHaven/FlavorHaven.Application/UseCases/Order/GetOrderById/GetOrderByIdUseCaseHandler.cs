using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Order.GetOrderById;

public class GetOrderByIdUseCaseHandler : IRequestHandler<GetOrderByIdUseCase, OrderDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderByIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDTO> Handle(GetOrderByIdUseCase request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            throw new EntityNotFoundException(nameof(Order), request.OrderId);
        }

        return _mapper.Map<OrderDTO>(order);
    }
}
using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Order.GetOrders;

public class GetOrdersUseCaseHandler : IRequestHandler<GetOrdersUseCase,IEnumerable<OrderDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrdersUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDTO>> Handle(GetOrdersUseCase request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetAllWithIncludesAsync(cancellationToken);
        return _mapper.Map<IEnumerable<OrderDTO>>(orders);
    }
}
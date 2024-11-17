using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Order.GetOrdersByStatus;

public class GetOrdersByStatusUseCaseHandler : IRequestHandler<GetOrdersByStatusUseCase, IEnumerable<OrderDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrdersByStatusUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDTO>> Handle(GetOrdersByStatusUseCase request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetByStatusId(request.StatusId, cancellationToken);
        
        return _mapper.Map<IEnumerable<OrderDTO>>(orders);
    }
}
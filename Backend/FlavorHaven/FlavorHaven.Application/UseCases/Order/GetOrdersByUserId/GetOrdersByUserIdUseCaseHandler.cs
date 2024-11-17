using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Order.GetOrdersByUserId;

public class GetOrdersByUserIdUseCaseHandler : IRequestHandler<GetOrdersByUserIdUseCase, IEnumerable<OrderDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrdersByUserIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDTO>> Handle(GetOrdersByUserIdUseCase request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetByUserId(request.UserId, cancellationToken);
        
        return _mapper.Map<IEnumerable<OrderDTO>>(orders);
    }
}
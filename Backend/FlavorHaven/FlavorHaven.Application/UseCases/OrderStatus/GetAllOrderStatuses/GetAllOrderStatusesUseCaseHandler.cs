using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.OrderStatus.GetAllOrderStatuses;

public class GetAllOrderStatusesUseCaseHandler : IRequestHandler<GetAllOrderStatusesUseCase, IEnumerable<OrderStatusDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllOrderStatusesUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderStatusDTO>> Handle(GetAllOrderStatusesUseCase request, CancellationToken cancellationToken)
    {
        var statuses = await _unitOfWork.OrderStatuses.GetAllAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<OrderStatusDTO>>(statuses);
    }
}
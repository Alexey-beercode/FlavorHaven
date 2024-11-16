using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.OrderStatus.GetOrderStatusById;

public class GetOrderStatusByIdUseCaseHandler : IRequestHandler<GetOrderStatusByIdUseCase, OrderStatusDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderStatusByIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderStatusDTO> Handle(GetOrderStatusByIdUseCase request, CancellationToken cancellationToken)
    {
        var status = await _unitOfWork.OrderStatuses.GetByIdAsync(request.Id, cancellationToken);
        if (status is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.OrderStatus), request.Id);
        }

        return _mapper.Map<OrderStatusDTO>(status);
    }
}
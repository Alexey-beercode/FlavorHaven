using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.OrderStatus.CreateOrderStatus;

public class CreateOrderStatusUseCaseHandler : IRequestHandler<CreateOrderStatusUseCase>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderStatusUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(CreateOrderStatusUseCase request, CancellationToken cancellationToken)
    {
        var status = await _unitOfWork.OrderStatuses.GetByNameAsync(request.Name, cancellationToken);
        if (status is not null)
        {
            throw new EntityAlreadyExistsException(nameof(Domain.Entities.OrderStatus), request.Name);
        }

        status = _mapper.Map<Domain.Entities.OrderStatus>(request);

        await _unitOfWork.OrderStatuses.CreateAsync(status, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
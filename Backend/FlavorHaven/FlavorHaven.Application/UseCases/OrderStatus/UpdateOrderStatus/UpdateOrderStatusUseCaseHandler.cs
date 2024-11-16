using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.OrderStatus.UpdateOrderStatus;

public class UpdateOrderStatusUseCaseHandler : IRequestHandler<UpdateOrderStatusUseCase>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateOrderStatusUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateOrderStatusUseCase request, CancellationToken cancellationToken)
    {
        var status = await _unitOfWork.OrderStatuses.GetByNameAsync(request.Name, cancellationToken);
        if (status is not null)
        {
            throw new EntityAlreadyExistsException(nameof(Domain.Entities.OrderStatus), request.Name);
        }
        
        status = await _unitOfWork.OrderStatuses.GetByIdAsync(request.Id, cancellationToken);
        if (status is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.OrderStatus), request.Id);
        }
        
        _mapper.Map(request, status);
        
         _unitOfWork.OrderStatuses.Update(status);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Payment.GetPaymentByOrderId;

public class GetPaymentByOrderIdUseCaseHandler : IRequestHandler<GetPaymentByOrderIdUseCase, PaymentDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaymentByOrderIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaymentDTO> Handle(GetPaymentByOrderIdUseCase request, CancellationToken cancellationToken)
    {
        var payment = await _unitOfWork.Payments.GetByOrderId(request.OrderId, cancellationToken);
        if (payment == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Payment), request.OrderId);
        }

        return _mapper.Map<PaymentDTO>(payment);
    }
}
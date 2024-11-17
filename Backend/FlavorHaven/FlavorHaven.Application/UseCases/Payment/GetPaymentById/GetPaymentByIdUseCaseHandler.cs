using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Payment.GetPaymentById;

public class GetPaymentByIdUseCaseHandler : IRequestHandler<GetPaymentByIdUseCase, PaymentDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaymentByIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaymentDTO> Handle(GetPaymentByIdUseCase request, CancellationToken cancellationToken)
    {
        var payment = await _unitOfWork.Payments.GetByIdAsync(request.PaymentId, cancellationToken);
        if (payment is null)
        {
            throw new EntityNotFoundException(nameof(Payment), request.PaymentId);
        }

        return _mapper.Map<PaymentDTO>(payment);
    }
}
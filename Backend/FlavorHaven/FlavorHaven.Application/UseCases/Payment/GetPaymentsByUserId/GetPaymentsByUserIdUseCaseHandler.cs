using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Payment.GetPaymentsByUserId;

public class GetPaymentsByUserIdUseCaseHandler : IRequestHandler<GetPaymentsByUserIdUseCase, IEnumerable<PaymentDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaymentsByUserIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PaymentDTO>> Handle(GetPaymentsByUserIdUseCase request, CancellationToken cancellationToken)
    {
        var payments = await _unitOfWork.Payments.GetByUserId(request.UserId, cancellationToken);
        
        return _mapper.Map<IEnumerable<PaymentDTO>>(payments);
    }
}
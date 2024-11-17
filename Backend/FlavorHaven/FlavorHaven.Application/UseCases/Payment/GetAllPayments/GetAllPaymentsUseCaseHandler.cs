using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Payment.GetAllPayments;

public class GetAllPaymentsUseCaseHandler : IRequestHandler<GetAllPaymentsUseCase, IEnumerable<PaymentDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllPaymentsUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PaymentDTO>> Handle(GetAllPaymentsUseCase request, CancellationToken cancellationToken)
    {
        var payments = await _unitOfWork.Payments.GetAllAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<PaymentDTO>>(payments);
    }
}
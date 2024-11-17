using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Payment.GetPaymentByOrderId;

public class GetPaymentByOrderIdUseCase : IRequest<PaymentDTO>
{
    public Guid OrderId { get; set; }
}

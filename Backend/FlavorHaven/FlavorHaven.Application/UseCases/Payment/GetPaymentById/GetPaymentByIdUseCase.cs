using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Payment.GetPaymentById;

public class GetPaymentByIdUseCase : IRequest<PaymentDTO>
{
    public Guid PaymentId { get; set; }
}
using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Payment.GetPaymentsByUserId;

public class GetPaymentsByUserIdUseCase : IRequest<IEnumerable<PaymentDTO>>
{
    public Guid UserId { get; set; }
}
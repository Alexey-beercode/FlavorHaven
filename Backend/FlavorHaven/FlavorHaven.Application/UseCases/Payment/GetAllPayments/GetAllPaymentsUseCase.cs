using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Payment.GetAllPayments;

public class GetAllPaymentsUseCase : IRequest<IEnumerable<PaymentDTO>>
{
}
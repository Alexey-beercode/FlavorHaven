using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Payment.CreatePayment;

public class CreatePaymentUseCase : IRequest
{
    public Guid OrderId { get; set; }
}
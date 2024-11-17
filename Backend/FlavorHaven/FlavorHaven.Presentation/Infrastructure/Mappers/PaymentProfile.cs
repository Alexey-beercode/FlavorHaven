using AutoMapper;
using FlavorHaven.Application.UseCases.Payment.CreatePayment;
using FlavorHaven.DTOs.Payment;

namespace FlavorHaven.Infrastructure.Mappers;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<CreatePaymentRequestDTO, CreatePaymentUseCase>();
    }
}
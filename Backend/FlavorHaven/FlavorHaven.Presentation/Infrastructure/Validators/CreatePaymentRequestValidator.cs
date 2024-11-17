using FlavorHaven.DTOs.Payment;
using FluentValidation;

namespace FlavorHaven.Infrastructure.Validators;

public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequestDTO>
{
    public CreatePaymentRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();
    }
}

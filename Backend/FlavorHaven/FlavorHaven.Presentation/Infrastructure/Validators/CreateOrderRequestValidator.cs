using FlavorHaven.DTOs.Order;
using FluentValidation;

namespace FlavorHaven.Infrastructure.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequestDTO>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Note)
            .MaximumLength(500);
    }
}
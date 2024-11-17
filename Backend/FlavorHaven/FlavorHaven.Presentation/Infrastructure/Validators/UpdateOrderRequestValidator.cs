using FlavorHaven.DTOs.Order;
using FluentValidation;

namespace FlavorHaven.Infrastructure.Validators;

public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequestDTO>
{
    public UpdateOrderRequestValidator()
    {
        RuleFor(x => x.StatusId)
            .NotEmpty();
    }
}
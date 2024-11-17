using FlavorHaven.DTOs.OrderStatus;
using FluentValidation;

namespace FlavorHaven.Infrastructure.Validators;

public class OrderStatusRequestValidator : AbstractValidator<OrderStatusRequestDTO>
{
    public OrderStatusRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}

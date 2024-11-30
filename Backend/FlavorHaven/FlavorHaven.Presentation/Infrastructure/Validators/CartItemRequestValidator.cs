using FlavorHaven.DTOs.Cart;
using FluentValidation;

namespace FlavorHaven.Infrastructure.Validators;

public class CartItemRequestValidator : AbstractValidator<CartItemRequestDTO>
{
    public CartItemRequestValidator()
    {
        RuleFor(x => x.DishId)
            .NotEmpty();

        RuleFor(x => x.Count)
            .GreaterThan(0);
    }
}
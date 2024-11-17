using FlavorHaven.DTOs.Dish;
using FluentValidation;

namespace FlavorHaven.Infrastructure.Validators;

public class CreateDishRequestValidator : AbstractValidator<DishRequestDTO>
{
    public CreateDishRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .MaximumLength(2000)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _));

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}
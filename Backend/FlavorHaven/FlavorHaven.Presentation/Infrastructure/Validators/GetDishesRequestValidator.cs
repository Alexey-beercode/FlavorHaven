using FlavorHaven.DTOs.Dish;
using FluentValidation;

namespace FlavorHaven.Infrastructure.Validators;

public class GetDishesRequestValidator : AbstractValidator<GetDishesRequestDTO>
{
    public GetDishesRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .When(x => x.PageNumber.HasValue);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .When(x => x.PageSize.HasValue);

        RuleFor(x => x.SearchName)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.SearchName));
    }
}
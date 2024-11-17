using FlavorHaven.DTOs.DishCategory;
using FluentValidation;

namespace FlavorHaven.Infrastructure.Validators;

public class DishCategoryRequestValidator : AbstractValidator<DishCategoryRequestDTO>
{
    public DishCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
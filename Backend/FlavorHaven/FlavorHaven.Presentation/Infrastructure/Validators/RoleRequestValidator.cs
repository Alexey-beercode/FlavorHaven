using FlavorHaven.DTOs.Role;
using FluentValidation;

namespace FlavorHaven.Infrastructure.Validators;

public class RoleRequestValidator : AbstractValidator<RoleRequestDTO>
{
    public RoleRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
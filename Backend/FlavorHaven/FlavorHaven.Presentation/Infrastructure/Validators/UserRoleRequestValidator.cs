using FlavorHaven.DTOs.Role;
using FluentValidation;

namespace FlavorHaven.Infrastructure.Validators;

public class UserRoleRequestValidator : AbstractValidator<UserRoleRequestDTO>
{
    public UserRoleRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.RoleId)
            .NotEmpty();
    }
}
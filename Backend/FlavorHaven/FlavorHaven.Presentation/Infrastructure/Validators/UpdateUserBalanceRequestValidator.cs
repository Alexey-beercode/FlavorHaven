using FlavorHaven.DTOs.User;
using FluentValidation;

namespace FlavorHaven.Infrastructure.Validators;

public class UpdateUserBalanceRequestValidator : AbstractValidator<UpdateUserBalanceRequestDTO>
{
    public UpdateUserBalanceRequestValidator()
    {
        RuleFor(x => x.Count)
            .NotEmpty();
    }
}
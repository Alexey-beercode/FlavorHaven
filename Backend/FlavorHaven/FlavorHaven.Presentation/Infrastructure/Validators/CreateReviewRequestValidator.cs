using FlavorHaven.DTOs.Review;
using FluentValidation;

namespace FlavorHaven.Infrastructure.Validators;

public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequestDTO>
{
    public CreateReviewRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();

        RuleFor(x => x.Note)
            .NotEmpty()
            .MaximumLength(1000);
    }
}
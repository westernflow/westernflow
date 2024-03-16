using Business.Models.Inputs;
using FluentValidation;

namespace Business.Validators;

public class CreateCourseReviewInputValidator : AbstractValidator<CreateCourseReviewInput>
{
    public CreateCourseReviewInputValidator()
    {
        RuleFor(input => input.EasyRating)
            .InclusiveBetween(1, 5)
            .WithMessage("Easy rating should be between 1 and 5.");

        RuleFor(input => input.UsefulRating)
            .InclusiveBetween(1, 5)
            .WithMessage("Useful rating should be between 1 and 5.");
        
        RuleFor(input => input.ReviewText)
            .MaximumLength(1000)
            .WithMessage("Review text should be less than 1000 characters.");
    }
}
using Business.Interfaces;
using Business.Models.Inputs;
using FluentValidation;
using Repositories.Interfaces;

namespace Business.Validators;

public class CreateCourseReviewInputValidator : AbstractValidator<CreateCourseReviewInput>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseReviewRepository _courseReviewRepository;
    private readonly IProfessorRepository _professorRepository;
    private readonly IReviewerInfoProvider _reviewerInfoProvider;

    public CreateCourseReviewInputValidator(IProfessorRepository professorRepository,
        ICourseRepository courseRepository, IReviewerInfoProvider reviewerInfoProvider,
        ICourseReviewRepository courseReviewRepository)
    {
        _professorRepository = professorRepository;
        _courseRepository = courseRepository;
        _reviewerInfoProvider = reviewerInfoProvider;
        _courseReviewRepository = courseReviewRepository;

        RuleFor(input => input)
            .NotNull().WithMessage("Input cannot be null");

        RuleFor(input => input.ProfessorId)
            .MustAsync(ProfessorExistsAsync).WithMessage("Professor not found");

        RuleFor(input => input.CourseId)
            .MustAsync(CourseExistsAsync).WithMessage("Course not found");

        RuleFor(input => input)
            .MustAsync(ReviewerHasNotAlreadyReviewedAsync).WithMessage("Reviewer has already reviewed this course");
    }

    private async Task<bool> ReviewerHasNotAlreadyReviewedAsync(CreateCourseReviewInput input,
        CancellationToken cancellationToken)
    {
        var reviewer = await _reviewerInfoProvider.GetReviewer();
        var existingReview =
            await _courseReviewRepository.GetSingleOrDefaultAsync(r =>
                r.CourseId == input.CourseId && r.ReviewerId == reviewer.Id);
        return existingReview == null;
    }

    private async Task<bool> ProfessorExistsAsync(int? professorId, CancellationToken cancellationToken)
    {
        if (!professorId.HasValue)
            return true; // ProfessorId is optional, so it's considered valid if it's not provided

        var professor = await _professorRepository.GetSingleOrDefaultAsync(p => p.Id == professorId.Value);
        return professor != null;
    }

    private async Task<bool> CourseExistsAsync(int courseId, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetSingleOrDefaultAsync(c => c.Id == courseId);
        return course != null;
    }
}
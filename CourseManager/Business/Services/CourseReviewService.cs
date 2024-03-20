using Business.Interfaces;
using Business.Models.Inputs;
using Business.Validators;
using Data.Entities;
using FluentValidation;
using Repositories.Interfaces;

namespace Business.Services;

public class CourseReviewService : ICourseReviewService
{
    private readonly ICourseReviewRepository _courseReviewRepository;
    private readonly IReviewerInfoProvider _reviewerInfoProvider;
    private readonly CreateCourseReviewInputValidator _createCourseReviewInputValidator;

    public CourseReviewService(ICourseReviewRepository courseReviewRepository,
        IReviewerInfoProvider reviewerInfoProvider, CreateCourseReviewInputValidator createCourseReviewInputValidator)
    {
        _courseReviewRepository = courseReviewRepository;
        _reviewerInfoProvider = reviewerInfoProvider;
        _createCourseReviewInputValidator = createCourseReviewInputValidator;
    }

    public async Task<CourseReview> CreateCourseReviewAsync(CreateCourseReviewInput input)
    {
        await _createCourseReviewInputValidator.ValidateAndThrowAsync(input);

        var reviewer = await _reviewerInfoProvider.GetReviewer();

        var courseReview = new CourseReview
        {
            IsLiked = input.IsLiked,
            EasyRating = input.EasyRating,
            UsefulRating = input.UsefulRating,
            ReviewText = input.ReviewText ?? string.Empty,
            ReviewerId = reviewer.Id,
            CourseId = input.CourseId,
            ProfessorId = input.ProfessorId
        };

        await _courseReviewRepository.InsertAsync(courseReview);

        return courseReview;
    }
}
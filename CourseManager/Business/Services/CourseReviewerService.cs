using Business.Interfaces;
using Business.Models.Inputs;
using Business.Validators;
using Data.Entities;
using FluentValidation;
using Repositories.Interfaces;

namespace Business.Services;

public class CourseReviewerService : ICourseReviewerService
{
    private readonly ICourseReviewRepository _courseReviewRepository;
    private readonly IReviewerInfoProvider _reviewerInfoProvider;
    private readonly CreateCourseReviewInputValidator _createCourseReviewInputValidator;
    
    public CourseReviewerService(ICourseReviewRepository courseReviewRepository, IReviewerInfoProvider reviewerInfoProvider)
    {
        _courseReviewRepository = courseReviewRepository;
        _reviewerInfoProvider = reviewerInfoProvider;
        _createCourseReviewInputValidator = new CreateCourseReviewInputValidator();
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
            ProfessorId = input.ProfessorId,
            ReviewerId = reviewer.Id
        };
        
        await _courseReviewRepository.InsertAsync(courseReview);
        
        return courseReview;
    }
}
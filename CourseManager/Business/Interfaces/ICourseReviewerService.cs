using Business.Models.Inputs;
using Data.Entities;

namespace Business.Interfaces;

public interface ICourseReviewerService
{
    public Task<CourseReview> CreateCourseReviewAsync(
        CreateCourseReviewInput input);
}
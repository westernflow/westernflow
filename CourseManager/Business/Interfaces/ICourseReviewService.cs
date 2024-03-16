using Business.Models.Inputs;
using Data.Entities;

namespace Business.Interfaces;

public interface ICourseReviewService
{
    public Task<CourseReview> CreateCourseReviewAsync(
        CreateCourseReviewInput input);
}
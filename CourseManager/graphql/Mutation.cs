using Business.Interfaces;
using Business.Models.Inputs;
using Business.Services;
using Data.Entities;

namespace graphql;

public class Mutation
{
    public async Task<CourseReview> CreateCourseReviewAsync(
        [Service] ICourseReviewService courseReviewService,
        CreateCourseReviewInput input)
    {
        return await courseReviewService.CreateCourseReviewAsync(input);
    }
}
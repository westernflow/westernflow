using Data.Entities;

namespace Business.Interfaces;

public interface IReviewerInfoProvider
{
    Task<Reviewer> GetReviewer();
    string GetSubjectId();
    string GetJWTAsync();
}
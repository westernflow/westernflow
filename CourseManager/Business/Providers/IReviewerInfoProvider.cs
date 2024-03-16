using Data.Entities;

namespace Business.Providers;

public interface IReviewerInfoProvider
{
    string GetReviewerEmail();
    Task<Reviewer> GetReviewerByEmailAsync(string email);
    Task<Reviewer> GetOrCreateReviewerAsync();
    Task<string> GetJWTAsync();
}
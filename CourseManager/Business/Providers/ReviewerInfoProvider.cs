using System.Security.Claims;
using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace Business.Providers;

public class ReviewerInfoProvider : IReviewerInfoProvider
{
    private readonly IHttpContextAccessor _accessor;
    private readonly ILogger<ReviewerInfoProvider> _logger;
    private readonly IReviewerRepository _reviewerRepository;

    public ReviewerInfoProvider(
        IHttpContextAccessor accessor,
        IReviewerRepository reviewerRepository,
        ILogger<ReviewerInfoProvider> logger)
    {
        _accessor = accessor;
        _reviewerRepository = reviewerRepository;
        _logger = logger;
    }

    public async Task<Reviewer> GetReviewer()
    {
        var subjectId = GetSubjectId();

        var reviewer = await _reviewerRepository.GetSingleOrDefaultAsync(r => r.SubjectId == subjectId);

        if (reviewer is null)
        {
            reviewer = new Reviewer
            {
                SubjectId = subjectId
            };

            await _reviewerRepository.InsertAsync(reviewer);
        }

        return reviewer;
    }

    public string GetSubjectId()
    {
        var authHeader = _accessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (authHeader is null)
        {
            _logger.LogError("Authorization header is missing");
            return string.Empty;
        }

        var token = authHeader.Split(' ').Last();

        _logger.LogInformation("Token: {token}", token);

        // get the subject identifier from the jwt token via claim type on httpcontext accessor
        var subjectIdentifier = _accessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

        _logger.LogInformation("Subject Identifier: {subjectIdentifier}", subjectIdentifier);

        return subjectIdentifier;
    }

    public string GetJWTAsync()
    {
        var authHeader = _accessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (authHeader is null)
        {
            _logger.LogError("Authorization header is missing");
            return string.Empty;
        }

        var token = authHeader.Split(' ').Last();
        return token;
    }
}
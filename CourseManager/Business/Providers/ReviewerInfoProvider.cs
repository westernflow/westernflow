using System.Security.Claims;
using Data.Entities;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Business.Providers;

public class ReviewerInfoProvider : IReviewerInfoProvider
{
    private readonly IHttpContextAccessor _accessor;
    private readonly IReviewerRepository _reviewerRepository;
    private readonly ILogger<ReviewerInfoProvider> _logger;
    
    public ReviewerInfoProvider(
        IHttpContextAccessor accessor,
        IReviewerRepository reviewerRepository,
        ILogger<ReviewerInfoProvider> logger)
    {
        _accessor = accessor;
        _reviewerRepository = reviewerRepository;
        _logger = logger;
    }
    
    public string GetReviewerEmail()
    {
        return _accessor.HttpContext.User.Claims.First(i => i.Type == "email").Value;
    }

    public async Task<Reviewer> GetReviewerByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<Reviewer> GetOrCreateReviewerAsync()
    {
        throw new NotImplementedException();
    }
    
    public async Task<string> GetJWTAsync()
    {
        var authHeader = _accessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        
        if (authHeader is null)
        {
            _logger.LogError("Authorization header is missing");
            return null;
        }
        
        var token = authHeader.Split(' ').Last();
        
        _logger.LogInformation("Token: {token}", token);
        
        // get the subject identifier from the jwt token via claim type on httpcontext accessor
        var subjectIdentifier = _accessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
        
        _logger.LogInformation("Subject Identifier: {subjectIdentifier}", subjectIdentifier);
        
        return token;
    }
}
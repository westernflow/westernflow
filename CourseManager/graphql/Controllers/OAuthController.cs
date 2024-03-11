using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace graphql.Controllers;

[Route("api/Oauth/")]
[ApiController]
public class OAuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    
    public OAuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpPost("exchange")]
    public async Task<IActionResult> ExchangeAuthorizationCode([FromBody] TokenExchangeRequestModel exchangeRequestModel)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("code", exchangeRequestModel.Code),
                new KeyValuePair<string, string>("client_id", _configuration["OAuth:Google:ClientId"]),
                new KeyValuePair<string, string>("client_secret", _configuration["OAuth:Google:ClientSecret"]),
                new KeyValuePair<string, string>("redirect_uri", exchangeRequestModel.RedirectUri),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
            }));

            var responseString = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenExchangeResponse>(responseString);

            return Ok(tokenResponse);
        }
    }
    
    [HttpPost("refresh")]    
    public async Task<IActionResult> RefreshToken([FromBody] TokenRefreshRequestModel exchangeRequestModel)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("refresh_token", exchangeRequestModel.RefreshToken),
                new KeyValuePair<string, string>("client_id", _configuration["OAuth:Google:ClientId"]),
                new KeyValuePair<string, string>("client_secret", _configuration["OAuth:Google:ClientSecret"]),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
            }));

            var responseString = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenRefreshResponse>(responseString);
            return Ok(tokenResponse);
        }
    }
    
    
    public class TokenExchangeRequestModel
    {
        public string Code { get; set; }
        public string RedirectUri { get; set; }
    }

    public class TokenExchangeResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
    
    public class TokenRefreshRequestModel
    {
        public string RefreshToken { get; set; }
    }
    
    public class TokenRefreshResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
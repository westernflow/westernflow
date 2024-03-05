using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace graphql.Controllers;

[Route("api/auth/")]
[ApiController]
public class OAuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    
    public OAuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpPost("google")]
    public async Task<IActionResult> ExchangeAuthorizationCode([FromBody] TokenRequestModel requestModel)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("code", requestModel.Code),
                new KeyValuePair<string, string>("client_id", _configuration["OAuth:Google:ClientId"]),
                new KeyValuePair<string, string>("client_secret", _configuration["OAuth:Google:ClientSecret"]),
                new KeyValuePair<string, string>("redirect_uri", requestModel.RedirectUri),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
            }));

            var responseString = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);

            return Ok(tokenResponse);
        }
    }
    
    [HttpPost("google/refresh")]    
    public async Task<IActionResult> RefreshToken([FromBody] TokenRequestModel requestModel)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("refresh_token", requestModel.Code),
                new KeyValuePair<string, string>("client_id", _configuration["OAuth:Google:ClientId"]),
                new KeyValuePair<string, string>("client_secret", _configuration["OAuth:Google:ClientSecret"]),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
            }));

            var responseString = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);
            Console.WriteLine($"New access token: {tokenResponse.AccessToken}");

            return Ok(tokenResponse);
        }
    }
    
    
    public class TokenRequestModel
    {
        public string Code { get; set; }
        public string RedirectUri { get; set; }
    }

    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
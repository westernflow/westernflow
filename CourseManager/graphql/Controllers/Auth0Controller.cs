using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace graphql.Controllers;

[Route("api/auth")]
[ApiController]
public class Auth0Controller : ControllerBase
{
    private readonly IConfiguration _configuration;
    
    public Auth0Controller(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet("private")]
    [Authorize]
    public IActionResult Private()
    {
        return Ok(new
        {
            Message = "Hello from a private endpoint! You need to be authenticated to see this."
        });
    } 
    
    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok(new
        {
            Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
        });
    }
}
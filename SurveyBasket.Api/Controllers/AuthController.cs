using Microsoft.Extensions.Options;
using SurveyBasket.Api.Authentication;

namespace SurveyBasket.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IOptions<JwtOptions> jwtOptions) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;


    [HttpPost("")]
    public async Task<IActionResult> LogInAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var authRequest = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
        return authRequest is null ? BadRequest("Invalid email/password") : Ok(authRequest);
    }
    [HttpGet("Test")]

    public IActionResult Test()
    {
        return Ok(_jwtOptions.Audience);
    }

}

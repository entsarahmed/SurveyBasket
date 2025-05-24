namespace SurveyBasket.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IConfiguration configuration) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost("")]
    public async Task<IActionResult> LogInAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var authRequest = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
        return authRequest is null ? BadRequest("Invalid email/password") : Ok(authRequest);
    }
  

}

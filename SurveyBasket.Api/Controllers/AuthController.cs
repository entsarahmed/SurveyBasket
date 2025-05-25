using Microsoft.Extensions.Options;
using SurveyBasket.Api.Authentication;

namespace SurveyBasket.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService,
    IOptions<JwtOptions> jwtOptions,
    IOptionsSnapshot<JwtOptions> optionsSnapshot,
    IOptionsMonitor<JwtOptions> optionsMonitor) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IOptionsSnapshot<JwtOptions> _optionsSnapshot = optionsSnapshot;
    private readonly IOptionsMonitor<JwtOptions> _optionsMonitor = optionsMonitor;
    private readonly IOptions<JwtOptions> _jwtOptions = jwtOptions;


    [HttpPost("")]
    public async Task<IActionResult> LogInAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var authRequest = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
        return authRequest is null ? BadRequest("Invalid email/password") : Ok(authRequest);
    }
    [HttpGet("Test")]

    public IActionResult Test()
    {
        var values = new {
            IOptionsValue = _jwtOptions.Value.ExpiryMinutes,
            IOptionSnapshot = _optionsSnapshot.Value.ExpiryMinutes,
            IOptionMonitor = _optionsMonitor.CurrentValue.ExpiryMinutes,
        };
        Thread.Sleep(5000);
        var values02 = new
        {
            IOptionsValue = _jwtOptions.Value.ExpiryMinutes,
            IOptionSnapshot = _optionsSnapshot.Value.ExpiryMinutes,
            IOptionMonitor = _optionsMonitor.CurrentValue.ExpiryMinutes,
        };
        return Ok(new
        {
            values,
            values02
        });
    }

}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DevelopmentController : ControllerBase
{
    private readonly IOS _iOS;

    public DevelopmentController(IOS ios)
    {
        _iOS =ios;
    }
    [HttpGet]
    public IActionResult Run()
    {
        var message = _iOS.RunApp();
        return Ok(message);
    }
}

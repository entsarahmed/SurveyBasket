using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DevelopmentController : ControllerBase
{
     private readonly ILogger _logger;

    public DevelopmentController( ILogger<DevelopmentController> logger)
    {
        
        _logger=logger;
    }


    [HttpGet]
    public IActionResult Run([FromKeyedServices("windows")] IOperationTransient windowsService,
        [FromKeyedServices("macOs")] IOperationTransient macOsService)
    {
        _logger.LogWarning("windows {0}", windowsService.OperationId);
        _logger.LogError("MacOs {0}", macOsService.OperationId);
        return Ok();
    }
}

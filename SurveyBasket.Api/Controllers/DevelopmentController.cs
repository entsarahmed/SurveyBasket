using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DevelopmentController : ControllerBase
{
    private readonly IOperationTransient _operationTransient;
    private readonly IOperationScoped _operationScoped;
    private readonly IOperationSingleton _operationSingleton;
    private readonly ILogger _logger;

    public DevelopmentController(IOperationTransient operationTransient, IOperationScoped operationScoped, IOperationSingleton operationSingleton, ILogger<DevelopmentController> logger)
    {
        _operationTransient=operationTransient;
        _operationScoped=operationScoped;
        _operationSingleton=operationSingleton;
        _logger=logger;
    }


    [HttpGet]
    public IActionResult Run()
    {
        _logger.LogInformation("Transient {0}", _operationTransient.OperationId);
        _logger.LogWarning("Scoped {0}",_operationScoped.OperationId);
        _logger.LogError("Singleton {0}", _operationSingleton.OperationId);
        
        return Ok();
    }
}

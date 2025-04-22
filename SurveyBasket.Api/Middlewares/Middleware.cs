using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Middlewares;

public class Middleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly IOperationSingleton _operationSingleton;

    public Middleware(RequestDelegate next, ILogger<Middleware> logger, IOperationSingleton operationSingleton)
    {
        _next=next;
        _logger=logger;
        _operationSingleton=operationSingleton;
    }
    public async Task InvokeAsync(HttpContext context, IOperationTransient operationTransient, IOperationScoped operationScoped)
    {
        _logger.LogInformation("Transient {0}", operationTransient.OperationId);
        _logger.LogWarning("Scoped {0}", operationScoped.OperationId);
        _logger.LogError("Singleton {0}", _operationSingleton.OperationId);
        await _next(context);
    }
}

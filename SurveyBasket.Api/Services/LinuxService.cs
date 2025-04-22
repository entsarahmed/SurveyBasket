namespace SurveyBasket.Api.Services;

public class LinuxService : IOperationTransient, IOperationScoped, IOperationSingleton
{
    public string OperationId { get; }
    public LinuxService()
    {
        OperationId = Guid.NewGuid().ToString()[^4..];
    }

    public string RunApp() => "Running from Linux";
}

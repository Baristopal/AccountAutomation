using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Utilities.Extensions;

namespace Core.CrossCuttingCorserns.Logging.Serilog;

public class LoggerManager : ILoggerService
{
    private readonly ILogger logger;
    public LoggerManager()
    {
        logger = DependencyInjectionExtensions.ServiceTool.ServiceProvider.GetService<ILogger>();
    }
    public void Error(string message)
    {
        logger.Log(LogLevel.Information, "{message}", message);
    }

    public void Error(Exception ex, string message)
    {
        logger.Log(LogLevel.Error, ex, "{message}", message);
    }

    public void Fatal(string message)
    {
        logger.Log(LogLevel.Critical, "{message}", message);
    }

    public void Fatal(Exception ex, string message)
    {
        logger.Log(LogLevel.Critical, ex, "{message}", message);
    }

    public void Info(string message)
    {
        logger.Log(LogLevel.Information, "{message}", message);
    }

    public void Warn(string message)
    {
        logger.Log(LogLevel.Warning, "{message}", message);
    }
}

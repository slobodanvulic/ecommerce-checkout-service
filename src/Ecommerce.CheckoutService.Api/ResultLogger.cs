using FluentResults;

namespace Ecommerce.CheckoutService.Api;

public class ResultLogger : IResultLogger
{
    private readonly ILogger _logger;

    public ResultLogger(ILogger logger)
    {
        _logger = logger;
    }

    public void Log(string context, string content, ResultBase result, LogLevel logLevel)
    {
        _logger.Log(logLevel, "{context}: {content}. {result}", context, content, result);
    }

    public void Log<TContext>(string content, ResultBase result, LogLevel logLevel)
    {
        _logger.Log(logLevel, "{context}: {content}. {result}", typeof(TContext).Name, content, result);
    }
}

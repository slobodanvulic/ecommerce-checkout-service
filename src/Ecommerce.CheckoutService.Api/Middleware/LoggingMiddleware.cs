using Microsoft.AspNetCore.Http.Extensions;
using System.Diagnostics;
using System.Text;

namespace Ecommerce.CheckoutService.Api.Middleware;

public class LoggingMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _contextAccessor;

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger, 
        RequestDelegate next, 
        IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _next = next;
        _contextAccessor = contextAccessor;
    }

    public async Task Invoke(HttpContext context)
    {
        var sw = new Stopwatch();
        sw.Start();

        var request = _contextAccessor.HttpContext!.Request;
        var response = _contextAccessor.HttpContext.Response;

        response.OnCompleted(() => 
            OnRequestCompleted(sw, request, response));

        await _next(context);
    }

    private Task OnRequestCompleted(Stopwatch stopwatch, HttpRequest request, HttpResponse response)
    {
        stopwatch.Stop();

        var message = new StringBuilder()
            .Append($"Request: {request.Method} {request.GetDisplayUrl()}. ")
            .Append($"Response status code: {response.StatusCode}. ")
            .Append($"Completed in {stopwatch.ElapsedMilliseconds}ms")
            .ToString();

        _logger.LogInformation(message);
        return Task.CompletedTask;
    }
}

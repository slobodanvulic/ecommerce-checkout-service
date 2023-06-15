using Ecommerce.CheckoutService.Api;
using Ecommerce.CheckoutService.Api.Middleware;
using Ecommerce.CheckoutService.Application;
using Ecommerce.CheckoutService.Infrastructure;
using FluentResults;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSimpleConsole();
builder.Services.AddControllers();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddHttpContextAccessor()
    .AddHealthChecks()
        .AddSqlServer(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();


app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionHandlerMiddleware>()
   .UseMiddleware<LoggingMiddleware>();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

var logger = app.Services
    .GetRequiredService<ILoggerFactory>()
    .CreateLogger("Ecommerce.CheckoutService.Api");
Result.Setup(rsb => { rsb.Logger = new ResultLogger(logger); });

app.Run();

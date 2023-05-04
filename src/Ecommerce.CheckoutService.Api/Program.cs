using Ecommerce.CheckoutService.Api;
using Ecommerce.CheckoutService.Api.Middleware;
using Ecommerce.CheckoutService.Application;
using Ecommerce.CheckoutService.Infrastructure;
using FluentResults;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSimpleConsole();
builder.Services.AddControllers();
builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddHttpContextAccessor();

var app = builder.Build();


app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionHandlerMiddleware>()
   .UseMiddleware<LoggingMiddleware>();

var logger = app.Services
    .GetRequiredService<ILoggerFactory>()
    .CreateLogger("Ecommerce.CheckoutService.Api");
Result.Setup(rsb => { rsb.Logger = new ResultLogger(logger); });

app.Run();

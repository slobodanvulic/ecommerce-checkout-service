using Ecommerce.CheckoutService.Api.Middleware;
using Ecommerce.CheckoutService.Application;
using Ecommerce.CheckoutService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
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

app.Run();

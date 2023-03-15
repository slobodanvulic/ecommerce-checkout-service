using Ecommerce.CheckoutService.Application.DomainClients;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.CheckoutService.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly))
            .AddDomainClients();
        return services;
    }
}

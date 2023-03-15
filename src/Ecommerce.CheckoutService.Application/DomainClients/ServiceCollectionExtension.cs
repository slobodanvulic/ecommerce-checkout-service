using Ecommerce.CheckoutService.Application.DomainClients.CustomerClient.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.CheckoutService.Application.DomainClients;

internal static class ServiceCollectionExtension
{
    public static IServiceCollection AddDomainClients(this IServiceCollection services)
    {
        services.AddTransient<ICustomerQueries, CustomerQueries>();

        return services;
    }
}

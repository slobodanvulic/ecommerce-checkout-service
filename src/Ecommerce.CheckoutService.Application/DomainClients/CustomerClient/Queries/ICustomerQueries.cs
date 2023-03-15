using Ecommerce.CheckoutService.Domain.Entities;

namespace Ecommerce.CheckoutService.Application.DomainClients.CustomerClient.Queries;

public interface ICustomerQueries
{
    Task<Customer> GetCustomerByIdAsync(Guid customerId);
}

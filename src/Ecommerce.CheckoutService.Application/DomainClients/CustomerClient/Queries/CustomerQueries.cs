using Ecommerce.CheckoutService.Domain.Entities;
using Ecommerce.CheckoutService.Domain.ValueObjects;

namespace Ecommerce.CheckoutService.Application.DomainClients.CustomerClient.Queries;

internal class CustomerQueries : ICustomerQueries
{
    public Task<Customer> GetCustomerByIdAsync(Guid customerId)
    {
        return Task.FromResult(
            new Customer(
                customerId,
                new Name("Foo", "Bar"),
                new Email("foo@bar.com"),
                new Address("London", "First Street", "50A")
                ));
    }
}

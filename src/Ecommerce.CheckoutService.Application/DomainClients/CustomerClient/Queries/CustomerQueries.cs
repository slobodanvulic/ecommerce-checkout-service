using Ecommerce.CheckoutService.Application.DomainClients.CustomerClient.Errors;
using Ecommerce.CheckoutService.Domain.Entities;
using Ecommerce.CheckoutService.Domain.ValueObjects;
using FluentResults;

namespace Ecommerce.CheckoutService.Application.DomainClients.CustomerClient.Queries;

internal class CustomerQueries : ICustomerQueries
{
    public Task<Result<Customer>> GetCustomerByIdAsync(Guid customerId, CancellationToken cancelationToken)
    {
        return Task.FromResult(
            Result.Ok(
                new Customer(
                    customerId,
                    new Name("Foo", "Bar"),
                    new Email("foo@bar.com"),
                    new Address("London", "First Street", "50A"),
                    customerId))
            .WithSuccess($"Successful call to Customers API to get customer with id {customerId}")
            .Log<CustomerQueries>());

        //return Task.FromResult(
        //    Result.Fail<Customer>(new CustomerNotFoundError(customerId))
        //    .Log<CustomerQueries>());
    }
}

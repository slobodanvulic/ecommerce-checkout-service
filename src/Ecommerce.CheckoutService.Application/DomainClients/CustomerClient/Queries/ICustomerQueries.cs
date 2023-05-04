using Ecommerce.CheckoutService.Domain.Entities;
using FluentResults;

namespace Ecommerce.CheckoutService.Application.DomainClients.CustomerClient.Queries;

public interface ICustomerQueries
{
    Task<Result<Customer>> GetCustomerByIdAsync(Guid customerId, CancellationToken cancelationToken);
}

using Ecommerce.CheckoutService.Application.Errors;

namespace Ecommerce.CheckoutService.Application.DomainClients.CustomerClient.Errors;

internal class CustomerNotFoundError : ClientError
{
    public CustomerNotFoundError(Guid customerId) : base()
    {
        Message = $"Customer with id {customerId} not found.";
    }
}

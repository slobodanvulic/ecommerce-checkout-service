using FluentResults;

namespace Ecommerce.CheckoutService.Application.Errors;

public abstract class ClientError : Error
{
    protected ClientError()
    {
        Metadata.Add(ErrorType.Client, "Error caused by invalid client data.");
    }
}

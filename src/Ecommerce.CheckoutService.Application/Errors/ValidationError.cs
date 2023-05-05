namespace Ecommerce.CheckoutService.Application.Errors;

public class ValidationError : ClientError
{
	public ValidationError(string message)
	{
		Message = message;
	}
}

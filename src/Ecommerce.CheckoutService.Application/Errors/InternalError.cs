using FluentResults;

namespace Ecommerce.CheckoutService.Application.Errors;

public abstract class InternalError : Error
{
	protected InternalError()
	{
		Metadata.Add(ErrorType.Internal, "Error caused by an internal issue.");
	}
}

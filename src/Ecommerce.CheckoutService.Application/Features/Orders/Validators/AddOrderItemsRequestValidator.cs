using Ecommerce.CheckoutService.Application.Features.Orders.Model;
using FluentValidation;

namespace Ecommerce.CheckoutService.Application.Features.Orders.Validators;

public class AddOrderItemsRequestValidator : AbstractValidator<AddOrderItemsRequest>
{
	public AddOrderItemsRequestValidator()
	{
		RuleFor(x => x.OrderItems)
			.NotNull()
			.NotEmpty();

		RuleForEach(x => x.OrderItems)
			.SetValidator(new OrderItemRequestValidator());
	}
}

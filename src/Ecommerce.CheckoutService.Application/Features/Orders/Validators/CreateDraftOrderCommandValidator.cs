using Ecommerce.CheckoutService.Application.Features.Orders.Commands;
using FluentValidation;

namespace Ecommerce.CheckoutService.Application.Features.Orders.Validators;

public class CreateDraftOrderCommandValidator : AbstractValidator<CreateDraftOrderCommand>
{
	public CreateDraftOrderCommandValidator()
	{
		RuleFor(x => x.OrderRequest)
			.NotNull()
			.SetValidator(new CreateDraftOrderRequestValidator());
	}
}

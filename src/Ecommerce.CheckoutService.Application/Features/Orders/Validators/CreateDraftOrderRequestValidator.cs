using Ecommerce.CheckoutService.Application.Features.Orders.Model;
using FluentValidation;

namespace Ecommerce.CheckoutService.Application.Features.Orders.Validators;

public class CreateDraftOrderRequestValidator : AbstractValidator<CreateDraftOrderRequest>
{
	public CreateDraftOrderRequestValidator()
	{
		RuleFor(x => x.CustomerId)
			.NotNull()
			.NotEmpty()
			.Must(BeGuid)
			.WithMessage("The CustomerId must me Guid.");
	}

    private bool BeGuid(string bar)
    {
        return Guid.TryParse(bar, out _);
    }
}

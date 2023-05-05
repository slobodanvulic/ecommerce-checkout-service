using Ecommerce.CheckoutService.Application.Features.Orders.Model;
using FluentValidation;

namespace Ecommerce.CheckoutService.Application.Features.Orders.Validators;

public class OrderItemRequestValidator : AbstractValidator<OrderItemRequest>
{
	public OrderItemRequestValidator()
	{
		RuleFor(x => x.ProductId)
            .NotNull()
            .NotEmpty()
            .Must(BeGuid)
            .WithMessage("The ProductId must me Guid.");

        RuleFor(x => x.Quantity)
            .Must(x => x > 0 && x < 100)
            .WithMessage("The Quantity must be greather than 0 and less than 100.");

        RuleFor(x => x.ProductPrice)
            .Must(x => x > 0m)
            .WithMessage("The ProductPrice must be greather than 0.");

        RuleFor(x => x.Discount)
            .Must(x => x >= 0m && x <= 1m)
            .WithMessage("The Discount coul not be less than 0 or greater than 1.");
    }

    private bool BeGuid(string bar)
    {
        return Guid.TryParse(bar, out _);
    }
}

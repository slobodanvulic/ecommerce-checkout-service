using Ecommerce.CheckoutService.Application.Features.Orders.Commands;
using FluentValidation;

namespace Ecommerce.CheckoutService.Application.Features.Orders.Validators;

public class AddOrderItemsCommandValidator : AbstractValidator<AddOrderItemsCommand>
{
    public AddOrderItemsCommandValidator()
    {
        RuleFor(x => x.OrderItems)
            .NotEmpty()
            .SetValidator(new AddOrderItemsRequestValidator());
    }
}

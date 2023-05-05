using Ecommerce.CheckoutService.Domain.Entities;

namespace Ecommerce.CheckoutService.Application.Features.Orders.Model;

public record OrderDetailsResponse(
    Guid Id,
    Guid CustomerId,
    OrderStatus Status,
    decimal Amount);


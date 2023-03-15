using Ecommerce.CheckoutService.Domain.Entities;

namespace Ecommerce.CheckoutService.Application.Model;

public record OrderResponse(
    Guid OrderId,
    Guid CustomerId,
    OrderStatus Status,
    decimal TotalAmount);


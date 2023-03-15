namespace Ecommerce.CheckoutService.Application.Model;

public record AddOrderItemsRequest(ICollection<OrderItemRequest> OrderItems);

public record OrderItemRequest(
    Guid ProductId,
    int Quantity,
    decimal ProductPrice,
    decimal Discount);
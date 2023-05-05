namespace Ecommerce.CheckoutService.Application.Features.Orders.Model;

public record AddOrderItemsRequest(ICollection<OrderItemRequest> OrderItems);

public record OrderItemRequest(
    string ProductId,
    int Quantity,
    decimal ProductPrice,
    decimal Discount);
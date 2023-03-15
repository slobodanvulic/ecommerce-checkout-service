using Ecommerce.CheckoutService.Domain.Entities;

namespace Ecommerce.CheckoutService.Application;

public interface IOrderRepository
{
    Task<Guid> SaveOrderAsync(Order order, CancellationToken cancellationToken);

    Task<Order?> GetOrderAsync(Guid orderId, CancellationToken cancellationToken);

}

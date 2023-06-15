using Ecommerce.CheckoutService.Domain.Entities;
using FluentResults;

namespace Ecommerce.CheckoutService.Application;

public interface IOrderRepository
{
    Task<Result<Order?>> GetOrderAsync(Guid orderId, CancellationToken cancellationToken);

    Result<Guid> AddOrder(Order order, CancellationToken cancellationToken);
}

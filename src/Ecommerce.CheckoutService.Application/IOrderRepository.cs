using Ecommerce.CheckoutService.Domain.Entities;
using FluentResults;

namespace Ecommerce.CheckoutService.Application;

public interface IOrderRepository
{
    Task<Result<Guid>> SaveOrderAsync(Order order, CancellationToken cancellationToken);

    Task<Result<Order?>> GetOrderAsync(Guid orderId, CancellationToken cancellationToken);

}

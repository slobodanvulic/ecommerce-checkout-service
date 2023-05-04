using Ecommerce.CheckoutService.Application;
using Ecommerce.CheckoutService.Domain.Entities;
using Ecommerce.CheckoutService.Domain.ValueObjects;
using FluentResults;

namespace Ecommerce.CheckoutService.Infrastructure.Repositories;

/// <summary>
/// This will be replaced with real databse 
/// </summary>
public class OrderRepository : IOrderRepository
{
    public Task<Result<Guid>> SaveOrderAsync(Order order, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            Result.Ok(order.Id)
            .WithSuccess($"Successcully saved order with id {order.Id}")
            .Log<OrderRepository>());
    }


    public Task<Result<Order?>> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Ok(
            new Order(
                orderId,
                new Customer(
                    Guid.NewGuid(),
                    new Name("Foo", "Bar"),
                    new Email("foo@bar.com"),
                    new Address("London", "First Street", "50A")
                    ),
                new List<OrderItem>()
                {
                    new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 5, 25m, 0),
                    new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 1, 13.2m, 0.2m),
                    new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 2, 104m, 0),
                }))
            .WithSuccess($"Successfully read order with id {orderId}")
            .Log<OrderRepository>())!;
    }
}

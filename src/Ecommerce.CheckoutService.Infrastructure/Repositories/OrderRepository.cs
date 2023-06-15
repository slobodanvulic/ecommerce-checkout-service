using Ecommerce.CheckoutService.Application;
using Ecommerce.CheckoutService.Domain.Entities;
using Ecommerce.CheckoutService.Infrastructure.Errors;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.CheckoutService.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IApplicationDbContext context;

    public OrderRepository(IApplicationDbContext context)
    {
        this.context = context;
    }


    public Result<Guid> AddOrder(Order order, CancellationToken cancellationToken)
    {
        context.Orders.Add(order);
        return Result.Ok(order.Id);
    }


    public async Task<Result<Order?>> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        Order? result;

        try
        {
            result = await context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .SingleOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Fail(new DatabaseError().CausedBy(ex))
                .Log<OrderRepository>($"Failed to get order {orderId}");
        }

        return Result.Ok(result);
    }
}

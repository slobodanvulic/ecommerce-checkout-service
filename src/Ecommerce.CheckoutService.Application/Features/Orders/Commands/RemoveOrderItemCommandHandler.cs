using Ecommerce.CheckoutService.Domain.Entities;
using FluentResults;
using FluentResults.Extensions;
using MediatR;

namespace Ecommerce.CheckoutService.Application.Features.Orders.Commands;

public record RemoveOrderItemCommand(Guid OrderId, Guid OrderItemId) : IRequest<Result>;
public class RemoveOrderItemCommandHandler : IRequestHandler<RemoveOrderItemCommand, Result>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveOrderItemCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        return await GetOrderAsync(request.OrderId, cancellationToken)
            .Bind(order => RemoveOrderItem(order, request.OrderItemId))
            .Bind(() => _unitOfWork.CommitAsync(cancellationToken));
    }

    private async Task<Result<Order>> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var orderResult = await _orderRepository.GetOrderAsync(orderId, cancellationToken);
        if (orderResult.IsFailed)
        {
            return orderResult.ToResult();
        }

        if (orderResult.Value is null)
        {
            return orderResult.WithError($"Order with given id {orderId} does not exist.")!;
        }

        return orderResult!;
    }

    private Result RemoveOrderItem(Order order, Guid orderItemId)
    {
        return order.RemoveOrderItem(orderItemId)
            ? Result.Ok()
            : Result.Fail($"Order item {orderItemId} does not exist in order {order.Id}");
    }
}
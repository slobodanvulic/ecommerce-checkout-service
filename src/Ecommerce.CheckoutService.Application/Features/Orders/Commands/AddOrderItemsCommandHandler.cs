using Ecommerce.CheckoutService.Application.Features.Orders.Model;
using Ecommerce.CheckoutService.Domain.Entities;
using FluentResults;
using FluentResults.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.CheckoutService.Application.Features.Orders.Commands;

public record AddOrderItemsCommand(Guid OrderId, AddOrderItemsRequest OrderItems) : IRequest<Result<OrderResponse>>;
public class AddOrderItemsCommandHandler : IRequestHandler<AddOrderItemsCommand, Result<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateDraftOrderCommandHandler> _logger;

    public AddOrderItemsCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateDraftOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<OrderResponse>> Handle(AddOrderItemsCommand request, CancellationToken cancellationToken)
    {
        return await GetOrderAsync(request.OrderId, cancellationToken)
            .Bind(order => AddOrderItems(order, request.OrderItems.OrderItems))
            .Bind(order => SaveChangesAsync(order.Id, cancellationToken))
            .Bind(CreateOrderResponse);
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

    private Result<Order> AddOrderItems(Order order, ICollection<OrderItemRequest> orderItems)
    {
        foreach (var orderItem in orderItems)
        {
            if(!Guid.TryParse(orderItem.ProductId, out var productId))
            {
                return Result.Fail($"ProductId must be guid - value is {orderItem.ProductId}");
            }

            order.AddOrderItem(
                productId,
                orderItem.Quantity,
                orderItem.ProductPrice,
                orderItem.Discount);
        }

        return Result.Ok(order);
    }

    private async Task<Result<Guid>> SaveChangesAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return (await _unitOfWork.CommitAsync(cancellationToken))
            .ToResult<Guid>(orderId);
    }

    private Result<OrderResponse> CreateOrderResponse(Guid orderId)
    {
        return Result.Ok(new OrderResponse(orderId));
    }
}

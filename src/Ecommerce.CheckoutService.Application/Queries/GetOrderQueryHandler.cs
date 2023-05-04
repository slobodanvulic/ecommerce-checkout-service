using Ecommerce.CheckoutService.Application.Model;
using Ecommerce.CheckoutService.Domain.Entities;
using FluentResults;
using FluentResults.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.CheckoutService.Application.Queries;

public record GetOrderQuery(Guid OrderId) : IRequest<Result<OrderDetailsResponse>>;
public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Result<OrderDetailsResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<GetOrderQueryHandler> _logger;

    public GetOrderQueryHandler(IOrderRepository orderRepository, ILogger<GetOrderQueryHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<Result<OrderDetailsResponse>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var result = await GetOrderAsync(request.OrderId, cancellationToken)
            .Bind(CreateResponse);

        return result;
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

    private Result<OrderDetailsResponse> CreateResponse(Order order) =>
        Result.Ok(new OrderDetailsResponse(
            order.Id,
            order.Customer.Id,
            order.Status,
            order.TotalAmount));
}

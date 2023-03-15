using Ecommerce.CheckoutService.Application.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.CheckoutService.Application.Queries;

public record GetOrderQuery(Guid OrderId) : IRequest<OrderResponse>;
public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<GetOrderQueryHandler> _logger;

    public GetOrderQueryHandler(IOrderRepository orderRepository, ILogger<GetOrderQueryHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderAsync(request.OrderId, cancellationToken);

        //check if order exists

        var orderToReturn = new OrderResponse(
            order.Id,
            order.Customer.Id,
            order.Status,
            order.TotalAmount);

        return orderToReturn;
    }
}

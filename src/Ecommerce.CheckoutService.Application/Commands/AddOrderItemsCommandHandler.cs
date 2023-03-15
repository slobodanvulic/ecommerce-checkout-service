using Ecommerce.CheckoutService.Application.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.CheckoutService.Application.Commands;

public record AddOrderItemsCommand(Guid OrderId, AddOrderItemsRequest OrderItems) : IRequest<OrderResponse>;
public class AddOrderItemsCommandHandler : IRequestHandler<AddOrderItemsCommand, OrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CreateDraftOrderCommandHandler> _logger;

    public AddOrderItemsCommandHandler(
        IOrderRepository orderRepository,
        ILogger<CreateDraftOrderCommandHandler> logger)
    {
        _orderRepository= orderRepository;
        _logger= logger;
    }


    public async Task<OrderResponse> Handle(AddOrderItemsCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            // this will be replaced with Result
            throw new Exception("Order does not exist");
        }

        foreach(var orderItem in request.OrderItems.OrderItems)
        {
            order.AddOrderItem(
                orderItem.ProductId, 
                orderItem.Quantity, 
                orderItem.ProductPrice, 
                orderItem.Discount);
        }


        var id = await _orderRepository.SaveOrderAsync(order, cancellationToken);


        var result = new OrderResponse(
            id,
            order.Customer.Id,
            order.Status,
            order.TotalAmount);


        return result;
    }
}

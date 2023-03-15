using Ecommerce.CheckoutService.Application.DomainClients.CustomerClient.Queries;
using Ecommerce.CheckoutService.Application.Model;
using Ecommerce.CheckoutService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.CheckoutService.Application.Commands;

public record CreateDraftOrderCommand(CreateDraftOrderRequest OrderRequest) : IRequest<OrderResponse>;
public class CreateDraftOrderCommandHandler : IRequestHandler<CreateDraftOrderCommand, OrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerQueries _customerQueries;
    private readonly ILogger<CreateDraftOrderCommandHandler> _logger;

    public CreateDraftOrderCommandHandler(
        IOrderRepository orderRepository,
        ICustomerQueries customerQueries,
        ILogger<CreateDraftOrderCommandHandler> logger)
    {
        _orderRepository= orderRepository;
        _customerQueries= customerQueries;
        _logger= logger;
    }


    public async Task<OrderResponse> Handle(CreateDraftOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerQueries.GetCustomerByIdAsync(request.OrderRequest.CustomerId);

        //check if customer exists

        var draftOrder = Order.NewDraft(Guid.NewGuid(), customer);

        var id = await _orderRepository.SaveOrderAsync(draftOrder, cancellationToken);


        var result = new OrderResponse(
            id,
            draftOrder.Customer.Id,
            draftOrder.Status,
            draftOrder.TotalAmount);

        return result;
    }
}

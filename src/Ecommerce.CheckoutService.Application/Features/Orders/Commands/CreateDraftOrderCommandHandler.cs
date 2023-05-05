using Ecommerce.CheckoutService.Application.DomainClients.CustomerClient.Queries;
using Ecommerce.CheckoutService.Application.Features.Orders.Model;
using Ecommerce.CheckoutService.Domain.Entities;
using FluentResults;
using FluentResults.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.CheckoutService.Application.Features.Orders.Commands;

public record CreateDraftOrderCommand(CreateDraftOrderRequest OrderRequest) : IRequest<Result<OrderResponse>>;
public class CreateDraftOrderCommandHandler : IRequestHandler<CreateDraftOrderCommand, Result<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerQueries _customerQueries;
    private readonly ILogger<CreateDraftOrderCommandHandler> _logger;

    public CreateDraftOrderCommandHandler(
        IOrderRepository orderRepository,
        ICustomerQueries customerQueries,
        ILogger<CreateDraftOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _customerQueries = customerQueries;
        _logger = logger;
    }


    public async Task<Result<OrderResponse>> Handle(CreateDraftOrderCommand request, CancellationToken cancellationToken)
    {
        return
            await _customerQueries.GetCustomerByIdAsync(Guid.Parse(request.OrderRequest.CustomerId), cancellationToken)
           .Bind(customer => Result.Ok(Order.NewDraft(Guid.NewGuid(), customer)))
           .Bind(order => _orderRepository.SaveOrderAsync(order, cancellationToken))
           .Bind(id => Result.Ok(new OrderResponse(id)));
    }
}

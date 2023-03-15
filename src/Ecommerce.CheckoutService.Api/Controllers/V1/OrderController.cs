using Ecommerce.CheckoutService.Application.Commands;
using Ecommerce.CheckoutService.Application.Model;
using Ecommerce.CheckoutService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.CheckoutService.Api.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IMediator mediator, ILogger<OrderController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }


    [HttpGet("{id}", Name = nameof(GetOrder))]
    public async Task<ActionResult<OrderResponse>> GetOrder(Guid id, CancellationToken cancellationToken)
    {
        var orderToReturn = await _mediator.Send(new GetOrderQuery(id), cancellationToken);
        return Ok(orderToReturn);
    }

    [HttpPost("create-draft")]
    public async Task<IActionResult> CreateDraftOrder([FromBody] CreateDraftOrderRequest order, CancellationToken cancellationToken)
    {
        var orderToReturn = await _mediator.Send(new CreateDraftOrderCommand(order), cancellationToken);

        return CreatedAtRoute(nameof(GetOrder), new { id = orderToReturn.OrderId }, orderToReturn);
    }

    [HttpPost("{orderId}/add-items")]
    public async Task<IActionResult> AddOrderItems(Guid orderId, [FromBody] AddOrderItemsRequest orderItems, CancellationToken cancellationToken)
    {
        var orderToReturn = await _mediator.Send(new AddOrderItemsCommand(orderId, orderItems), cancellationToken);

        return CreatedAtRoute(nameof(GetOrder), new { id = orderToReturn.OrderId }, orderToReturn);
    }
}

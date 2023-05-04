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
    public async Task<ActionResult<OrderDetailsResponse>> GetOrder(Guid id, CancellationToken cancellationToken)
    {
        var orderResult = (await _mediator.Send(new GetOrderQuery(id), cancellationToken))
            .Log<OrderController>("Execute GetOrderQuery");

        if (orderResult.IsSuccess)
        {
            return Ok(orderResult.Value);
        }

        return orderResult.MapErrorsToResponse();
    }

    [HttpPost("create-draft")]
    public async Task<IActionResult> CreateDraftOrder([FromBody] CreateDraftOrderRequest order, CancellationToken cancellationToken)
    {
        var orderResult = (await _mediator.Send(new CreateDraftOrderCommand(order), cancellationToken))
            .Log<OrderController>("Execute CreateDraftOrderCommand");

        if (orderResult.IsSuccess)
        {
            return CreatedAtRoute(nameof(GetOrder), new { id = orderResult.Value.Id }, orderResult.Value);
        }

        return orderResult.MapErrorsToResponse();
    }

    [HttpPost("{orderId}/add-items")]
    public async Task<IActionResult> AddOrderItems(Guid orderId, [FromBody] AddOrderItemsRequest orderItems, CancellationToken cancellationToken)
    {
        var orderResult = (await _mediator.Send(new AddOrderItemsCommand(orderId, orderItems), cancellationToken))
            .Log<OrderController>("Execute AddOrderItemsCommand");

        if (orderResult.IsSuccess)
        {
            return CreatedAtRoute(nameof(GetOrder), new { id = orderResult.Value.Id }, orderResult.Value);
        }

        return orderResult.MapErrorsToResponse();
    }
}

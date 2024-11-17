using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.UseCases.Order.CreateOrderFromCart;
using FlavorHaven.Application.UseCases.Order.DeleteOrder;
using FlavorHaven.Application.UseCases.Order.GetOrderById;
using FlavorHaven.Application.UseCases.Order.GetOrdersByStatus;
using FlavorHaven.Application.UseCases.Order.GetOrdersByUserId;
using FlavorHaven.Application.UseCases.Order.UpdateOrderStatus;
using FlavorHaven.DTOs.Order;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Controllers;

[ApiController]
[Route("api/order")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create-by-user-id/{userId}")]
    public async Task<IActionResult> CreateOrder(Guid userId, [FromBody] CreateOrderRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateOrderFromCartUseCase>(request);
        command.UserId = userId;
        
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
    
    [HttpGet("get-by-id/{orderId}")]
    public async Task<ActionResult<OrderDTO>> GetOrderById(Guid orderId, CancellationToken cancellationToken = default)
    {
        var query = new GetOrderByIdUseCase { OrderId = orderId };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-status/{statusId}")]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByStatus(Guid statusId, CancellationToken cancellationToken = default)
    {
        var query = new GetOrdersByStatusUseCase { StatusId = statusId };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-user/{userId}")]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        var query = new GetOrdersByUserIdUseCase { UserId = userId };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}
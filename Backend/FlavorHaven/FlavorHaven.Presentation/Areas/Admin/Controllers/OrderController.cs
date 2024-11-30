using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.UseCases.Order.CreateOrderFromCart;
using FlavorHaven.Application.UseCases.Order.DeleteOrder;
using FlavorHaven.Application.UseCases.Order.GetOrderById;
using FlavorHaven.Application.UseCases.Order.GetOrders;
using FlavorHaven.Application.UseCases.Order.GetOrdersByStatus;
using FlavorHaven.Application.UseCases.Order.GetOrdersByUserId;
using FlavorHaven.Application.UseCases.Order.UpdateOrderStatus;
using FlavorHaven.DTOs.Order;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Areas.Admin.Controllers;

[ApiController]
[Route("api/order")]
[Authorize(Policy = "AdminArea")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpDelete("delete/{orderId}")]
    public async Task<IActionResult> DeleteOrder(Guid orderId, CancellationToken cancellationToken = default)
    {
        var command = new DeleteOrderUseCase { OrderId = orderId };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpPut("update-status/{orderId}")]
    public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateOrderStatusUseCase>(request);
        command.OrderId = orderId;
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetOrdersUseCase();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}
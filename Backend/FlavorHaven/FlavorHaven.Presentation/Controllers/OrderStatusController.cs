using AutoMapper;
using FlavorHaven.Application.UseCases.OrderStatus.CreateOrderStatus;
using FlavorHaven.Application.UseCases.OrderStatus.DeleteOrderStatus;
using FlavorHaven.Application.UseCases.OrderStatus.GetAllOrderStatuses;
using FlavorHaven.Application.UseCases.OrderStatus.GetOrderStatusById;
using FlavorHaven.Application.UseCases.OrderStatus.UpdateOrderStatus;
using FlavorHaven.DTOs.OrderStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Controllers;

[ApiController]
[Route("api/order-status")]
public class OrderStatusController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderStatusController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllOrderStatuses(CancellationToken cancellationToken = default)
    {
        var query = new GetAllOrderStatusesUseCase();
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetOrderStatusById(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetOrderStatusByIdUseCase() { Id = id };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}
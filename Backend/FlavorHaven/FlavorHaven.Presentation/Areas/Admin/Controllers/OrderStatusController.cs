using AutoMapper;
using FlavorHaven.Application.UseCases.OrderStatus.CreateOrderStatus;
using FlavorHaven.Application.UseCases.OrderStatus.DeleteOrderStatus;
using FlavorHaven.Application.UseCases.OrderStatus.GetAllOrderStatuses;
using FlavorHaven.Application.UseCases.OrderStatus.GetOrderStatusById;
using FlavorHaven.Application.UseCases.OrderStatus.UpdateOrderStatus;
using FlavorHaven.DTOs.OrderStatus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Areas.Admin.Controllers;

[ApiController]
[Route("api/order-status")]
[Authorize(Policy = "AdminArea")]
public class OrderStatusController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderStatusController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrderStatus([FromBody] OrderStatusRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateOrderStatusUseCase>(request);
        
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteOrderStatus(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteOrderStatusUseCase() { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] OrderStatusRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateOrderStatusUseCase>(request);
        command.Id = id;
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}
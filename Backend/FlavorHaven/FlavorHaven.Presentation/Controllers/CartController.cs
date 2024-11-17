using AutoMapper;
using FlavorHaven.Application.UseCases.Cart.AddToCart;
using FlavorHaven.Application.UseCases.Cart.ClearCart;
using FlavorHaven.Application.UseCases.Cart.GetCartsByUserId;
using FlavorHaven.Application.UseCases.Cart.RemoveFromCart;
using FlavorHaven.DTOs.Cart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Controllers;

[ApiController]
[Route("api/cart")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CartController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("add-by-user-id/{userId}")]
    public async Task<IActionResult> AddToCart(Guid userId, [FromBody] CartItemRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<AddToCartUseCase>(request);
        command.UserId = userId;
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpDelete("remove-by-user-id/{userId}")]
    public async Task<IActionResult> RemoveFromCart(Guid userId, [FromBody] CartItemRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<RemoveFromCartUseCase>(request);
        command.UserId = userId;
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpDelete("clear-by-user-id/{userId}")]
    public async Task<IActionResult> ClearCart(Guid userId, CancellationToken cancellationToken = default)
    {
        var command = new ClearCartUseCase() { UserId = userId };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpGet("get-by-user-id/{userId}")]
    public async Task<IActionResult> GetCartsByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        var query = new GetCartsByUserIdUseCase() { UserId = userId };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}
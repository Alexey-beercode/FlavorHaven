using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.UseCases.Payment.CreatePayment;
using FlavorHaven.Application.UseCases.Payment.GetAllPayments;
using FlavorHaven.Application.UseCases.Payment.GetPaymentById;
using FlavorHaven.Application.UseCases.Payment.GetPaymentByOrderId;
using FlavorHaven.Application.UseCases.Payment.GetPaymentsByUserId;
using FlavorHaven.DTOs.Payment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Controllers;

[ApiController]
[Route("api/payment")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PaymentController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = new CreatePaymentUseCase { OrderId = request.OrderId };
        
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetAllPayments(CancellationToken cancellationToken = default)
    {
        var query = new GetAllPaymentsUseCase();
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-id/{paymentId}")]
    public async Task<ActionResult<PaymentDTO>> GetPaymentById(Guid paymentId, CancellationToken cancellationToken = default)
    {
        var query = new GetPaymentByIdUseCase { PaymentId = paymentId };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-order/{orderId}")]
    public async Task<ActionResult<PaymentDTO>> GetPaymentByOrderId(Guid orderId, CancellationToken cancellationToken = default)
    {
        var query = new GetPaymentByOrderIdUseCase { OrderId = orderId };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-user/{userId}")]
    public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        var query = new GetPaymentsByUserIdUseCase { UserId = userId };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}
using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.UseCases.Review.CreateReview;
using FlavorHaven.Application.UseCases.Review.DeleteReview;
using FlavorHaven.Application.UseCases.Review.GetReviewById;
using FlavorHaven.Application.UseCases.Review.GetReviewByOrderId;
using FlavorHaven.Application.UseCases.Review.GetReviewsByUserId;
using FlavorHaven.DTOs.Review;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Controllers;

[ApiController]
[Route("api/review")]
[Authorize]
public class ReviewController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ReviewController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateReviewUseCase>(request);
        
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("delete/{reviewId}")]
    public async Task<IActionResult> DeleteReview(Guid reviewId, CancellationToken cancellationToken = default)
    {
        var command = new DeleteReviewUseCase { ReviewId = reviewId };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpGet("get-by-id/{reviewId}")]
    public async Task<ActionResult<ReviewDTO>> GetReviewById(Guid reviewId, CancellationToken cancellationToken = default)
    {
        var query = new GetReviewByIdUseCase { ReviewId = reviewId };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-order/{orderId}")]
    public async Task<ActionResult<ReviewDTO>> GetReviewByOrderId(Guid orderId, CancellationToken cancellationToken = default)
    {
        var query = new GetReviewByOrderIdUseCase { OrderId = orderId };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-user/{userId}")]
    public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviewsByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        var query = new GetReviewsByUserIdUseCase { UserId = userId };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}
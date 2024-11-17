using AutoMapper;
using FlavorHaven.Application.UseCases.Auth.Login;
using FlavorHaven.Application.UseCases.Auth.RefreshToken;
using FlavorHaven.Application.UseCases.Auth.Register;
using FlavorHaven.Application.UseCases.Auth.Revoke;
using FlavorHaven.DTOs.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<LoginUseCase>(request);
        
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<RegisterUseCase>(request);
        
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }

    [HttpPost("refresh/{refreshToken}")]
    public async Task<IActionResult> RefreshToken(string refreshToken, CancellationToken cancellationToken = default)
    {
        var command = new RefreshTokenUseCase() {RefreshToken = refreshToken };
        
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }

    [Authorize]
    [HttpPost("revoke/{id}")]
    public async Task<IActionResult> Revoke(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new RevokeUseCase() { UserId = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}
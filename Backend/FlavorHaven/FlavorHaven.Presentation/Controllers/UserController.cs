using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.UseCases.User.DeleteUser;
using FlavorHaven.Application.UseCases.User.GetAllUsers;
using FlavorHaven.Application.UseCases.User.GetUserById;
using FlavorHaven.Application.UseCases.User.UpdateUserBalance;
using FlavorHaven.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers(CancellationToken cancellationToken = default)
    {
        var query = new GetAllUsersUseCase();
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult<UserDTO>> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetUserByIdUseCase { Id = id };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpPut("update-balance/{id}")]
    public async Task<IActionResult> UpdateUserBalance(Guid id, [FromBody] UpdateUserBalanceRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = new UpdateUserBalanceUseCase 
        { 
            Id = id,
            Count = request.Count
        };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}
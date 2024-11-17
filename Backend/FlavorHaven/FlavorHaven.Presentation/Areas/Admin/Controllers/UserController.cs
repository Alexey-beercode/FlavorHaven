using AutoMapper;
using FlavorHaven.Application.UseCases.User.DeleteUser;
using FlavorHaven.Application.UseCases.User.UpdateUserBalance;
using FlavorHaven.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Areas.Admin.Controllers;

[ApiController]
[Route("api/user")]
[Authorize(Policy = "AdminArea")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteUserUseCase { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
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
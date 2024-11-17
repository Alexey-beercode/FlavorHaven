using AutoMapper;
using FlavorHaven.Application.UseCases.Role.CreateRole;
using FlavorHaven.Application.UseCases.Role.DeleteRole;
using FlavorHaven.Application.UseCases.Role.RemoveRoleFromUser;
using FlavorHaven.Application.UseCases.Role.SetRoleToUser;
using FlavorHaven.Application.UseCases.Role.UpdateRole;
using FlavorHaven.DTOs.Role;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Areas.Admin.Controllers;

[ApiController]
[Route("api/role")]
[Authorize(Policy = "AdminArea")]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RoleController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRole([FromBody] RoleRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateRoleUseCase>(request);
        
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteRole(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteRoleUseCase { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpPost("set-role-to-user")]
    public async Task<IActionResult> SetRoleToUser([FromBody] UserRoleRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<SetRoleToUserUseCase>(request);
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpPost("remove-role-from-user")]
    public async Task<IActionResult> RemoveRoleFromUser([FromBody] UserRoleRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<RemoveRoleFromUserUseCase>(request);
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateRole(Guid id, [FromBody] RoleRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateRoleUseCase>(request);
        command.Id = id;
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}
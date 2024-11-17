using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.UseCases.Role.GetAllRoles;
using FlavorHaven.Application.UseCases.Role.GetRoleById;
using FlavorHaven.Application.UseCases.Role.GetRoleByName;
using FlavorHaven.Application.UseCases.Role.GetRolesByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Controllers;

[ApiController]
[Route("api/role")]
[Authorize]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RoleController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAllRoles(CancellationToken cancellationToken = default)
    {
        var query = new GetAllRolesUseCase();
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult<RoleDTO>> GetRoleById(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetRoleByIdUseCase { Id = id };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-name/{name}")]
    public async Task<ActionResult<RoleDTO>> GetRoleByName(string name, CancellationToken cancellationToken = default)
    {
        var query = new GetRoleByNameUseCase { Name = name };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-user/{userId}")]
    public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRolesByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        var query = new GetRolesByUserIdUseCase { UserId = userId };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}
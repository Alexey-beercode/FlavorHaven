using AutoMapper;
using FlavorHaven.Application.UseCases.Dish.CreateDish;
using FlavorHaven.Application.UseCases.Dish.DeleteDish;
using FlavorHaven.Application.UseCases.Dish.UpdateDish;
using FlavorHaven.DTOs.Dish;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Areas.Admin.Controllers;

[ApiController]
[Route("api/dish")]
[Authorize(Policy = "AdminArea")]
public class DishController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DishController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateDish([FromBody] DishRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateDishUseCase>(request);
        
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteDish(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteDishUseCase { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateDish(Guid id, [FromBody] DishRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateDishUseCase>(request);
        command.Id = id;
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}
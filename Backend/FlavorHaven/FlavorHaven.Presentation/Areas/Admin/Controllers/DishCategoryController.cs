using AutoMapper;
using FlavorHaven.Application.UseCases.DishCategory.CreateDishCategory;
using FlavorHaven.Application.UseCases.DishCategory.DeleteDishCategory;
using FlavorHaven.Application.UseCases.DishCategory.UpdateDishCategory;
using FlavorHaven.DTOs.DishCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Areas.Admin.Controllers;

[ApiController]
[Route("api/dish-category")]
[Authorize(Policy = "AdminArea")]
public class DishCategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DishCategoryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateDishCategory([FromBody] DishCategoryRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateDishCategoryUseCase>(request);
        
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteDishCategory(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteDishCategoryUseCase() { Id = id };
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateDishCategory(Guid id, [FromBody] DishCategoryRequestDTO request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateDishCategoryUseCase>(request);
        command.Id = id;
        
        await _mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}
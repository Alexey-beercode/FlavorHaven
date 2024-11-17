using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.Models.Results;
using FlavorHaven.Application.UseCases.Dish.CreateDish;
using FlavorHaven.Application.UseCases.Dish.DeleteDish;
using FlavorHaven.Application.UseCases.Dish.GetDishById;
using FlavorHaven.Application.UseCases.Dish.GetDishes;
using FlavorHaven.Application.UseCases.Dish.UpdateDish;
using FlavorHaven.DTOs.Dish;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Controllers;

[ApiController]
[Route("api/dish")]
public class DishController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DishController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetDishById(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetDishByIdUseCase { Id = id };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpPost("get-by-parameters")]
    public async Task<ActionResult<PaginatedResult<DishDTO>>> GetDishes([FromBody] GetDishesRequestDTO request, CancellationToken cancellationToken = default)
    {
        var query = _mapper.Map<GetDishesUseCase>(request);
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}
using AutoMapper;
using FlavorHaven.Application.UseCases.DishCategory.CreateDishCategory;
using FlavorHaven.Application.UseCases.DishCategory.DeleteDishCategory;
using FlavorHaven.Application.UseCases.DishCategory.GetAllDishCategories;
using FlavorHaven.Application.UseCases.DishCategory.GetDishCategoriesById;
using FlavorHaven.Application.UseCases.DishCategory.UpdateDishCategory;
using FlavorHaven.DTOs.DishCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlavorHaven.Controllers;

[ApiController]
[Route("api/dish-category")]
public class DishCategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DishCategoryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllDishCategories(CancellationToken cancellationToken = default)
    {
        var query = new GetAllDishCategoriesUseCase();
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetDishCategoryById(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetDishCategoriesByIdUseCase() { Id = id };
        
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}
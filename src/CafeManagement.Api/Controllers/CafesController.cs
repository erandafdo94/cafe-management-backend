using System.ComponentModel.DataAnnotations;
using CafeManagement.Application.Cafes.Commands.DeleteCafe;
using CafeManagement.Application.Cafes.Queries;
using CafeManagement.Application.Dto;
using CafeManagement.Application.Dto.UpdateCafe;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Api.Controllers;

[ApiController]
[Route("api/cafe")]
public class CafesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CafesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CafeDto>>> GetCafes([FromQuery] string? location)
    {
        var query = new GetCafesByLocationQuery { Location = location };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCafe(CreateCafeCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    
    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] Guid id, [FromBody] UpdateCafeCommand command)
    {
        try
        {
            command = command with { Id = id }; // Set the ID from query parameter
            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }
        catch (ValidationException ex)
        {
            return BadRequest("Invalid data");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteCafeCommand(id));
        return result ? Ok() : NotFound();
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CafeDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllCafesQuery());
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CafeDto>> GetById(Guid id)
    {
        var query = new GetCafeByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
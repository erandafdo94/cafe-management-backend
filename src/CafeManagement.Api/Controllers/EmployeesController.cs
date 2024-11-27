using System.ComponentModel.DataAnnotations;
using CafeManagement.Application.Dto;
using CafeManagement.Application.Employee.Command.DeleteEmployee;
using CafeManagement.Application.Employee.Command.UpdateEmployee;
using CafeManagement.Application.Employee.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<string>> Create(CreateEmployeeCommand command)
    {
        try
        {
            var employeeId = await _mediator.Send(command);
            return Ok(employeeId);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees([FromQuery] Guid? cafe)
    {
        var query = new GetEmployeesListQuery { CafeId = cafe };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] string id, [FromBody] UpdateEmployeeCommand command)
    {
        try
        {
            var updateCommand = new UpdateEmployeeCommand
            {
                Id = id,
                Name = command.Name,
                EmailAddress = command.EmailAddress,
                PhoneNumber = command.PhoneNumber,
                Gender = command.Gender,
                CafeId = command.CafeId
            };
            
            var result = await _mediator.Send(updateCommand);
            return result ? Ok() : NotFound();
        }
        catch (ValidationException ex)
        {
            return BadRequest("Validation error: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(new DeleteEmployeeCommand(id));
        return result ? Ok() : NotFound();
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllEmployeesQuery());
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetById(Guid id)
    {
        var query = new GetEmployeeByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
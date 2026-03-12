// AutoMapper;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using TickBank.Application.Features.Reminders.DTOs;
using TickBank.Infrastructure.Persistence.Database;
using TickBank.Domain.Entities;
using TickBank.Application.Features.Reminders.Queries;
using System.Threading.Tasks;
using TickBank.Application.Features.Reminders.Commands;

namespace TickBank.Web.Controllers;

[ApiController]
[Route("api/reminders")]
public class RemindersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;

    public RemindersController(ApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getReminders    ")]

    public async Task<IActionResult> GetAllReminders()
    {
        var query = new GetAllRemindersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("getReminderById/{id}")]

    public IActionResult GetReminderById(Guid id)
    {
        var query = new GetReminderByIdQuery(id);
        var result = _mediator.Send(query);
        return Ok(result);

    }

    [HttpPost]
    [Route("createReminder")]

    public async Task<IActionResult> CreateReminder([FromBody] CreateReminderCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetReminderById), new { id = result.Id }, result);
    }



    [HttpPut]
    [Route("updateReminder/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReminderCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route ID and command ID do not match.");

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete]
    [Route("deleteReminder/{id}")]

    public IActionResult DeleteReminder(Guid id)
    {
        var reminder = _context.Reminders.Find(id);
        if (reminder == null)
        {
            return NotFound();
        }

        _context.Reminders.Remove(reminder);
        _context.SaveChanges();

        return NoContent();
    }
}
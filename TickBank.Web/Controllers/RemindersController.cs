// AutoMapper;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using TickBank.Application.Features.Reminders.DTOs;
using TickBank.Infrastructure.Persistence.Database;
using TickBank.Domain.Entities;
using TickBank.Application.Features.Reminders.Queries;

namespace TickBank.Web.Controllers;

[ApiController]
[Route("api/reminders")]
public class RemindersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;

    public RemindersController(ApplicationDbContext context , IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReminders()
    {
        var query = new GetAllRemindersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetReminderById(Guid id)
    {
        var reminder = _context.Reminders.Find(id);
        if (reminder == null)
        {
            return NotFound();
        }

        return Ok(reminder);
    }

   [HttpPost]
   public IActionResult Createreminder()
   {
       var reminder = new Domain.Entities.Reminder
       {
           Title = "Title",
           Category =" reminderDto.Category",
           Hours = 1,
           Date = DateTime.Now,
         
       };

       _context.Reminders.Add(reminder);
       _context.SaveChanges();

       return Ok(reminder);
   }
   
   [HttpPut("{id}")]
    public IActionResult UpdateReminder(Guid id, ReminderDto reminderDto)
    {
        var existingReminder = _context.Reminders.Find(id);
        if (existingReminder == null)
        {
            return NotFound();
        }

        existingReminder.Title = reminderDto.Title;
        existingReminder.Category = reminderDto.Category;
        existingReminder.Hours = reminderDto.Hours;
        existingReminder.Date = reminderDto.Date;

        // Update ranges
    //    existingReminder.Ranges.Clear();
       // foreach (var rangeDto in reminderDto.Ranges)
      //  {
       //     existingReminder.Ranges.Add(new Domain.Entities.ReminderRange
       //     {
           //     StartTime = rangeDto.StartTime,
           //     EndTime = rangeDto.EndTime
        //    });
      //  }

        _context.SaveChanges();

        return Ok(existingReminder);
    }
    
    [HttpDelete("{id}")]
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
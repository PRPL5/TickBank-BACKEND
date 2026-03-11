using MediatR;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickBank.Application.Features.ReminderRanges.DTOs;
using TickBank.Application.Features.Reminders.Commands;
using TickBank.Infrastructure.Persistence.Database;

namespace TickBank.Application.Features.Reminders.Handlers
{
    public class UpdateReminderHandler :IRequestHandler<UpdateReminderCommand , DTOs.ReminderDto>
    {
        private readonly ApplicationDbContext _context;

        public UpdateReminderHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DTOs.ReminderDto> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
        {
            var reminder = await _context.Reminders.FindAsync(request.Id);
            if (reminder == null)
            {
                throw new Exception($"Reminder with ID '{request.Id}' was not found.");
            }
            reminder.Title = request.Title;
            reminder.Category = request.Category;
            reminder.Hours = request.Hours;
            reminder.Date = request.Date;
            _context.Ranges.RemoveRange(reminder.Ranges);

            reminder.Ranges = request.Ranges.Select(r => new Domain.Entities.ReminderRange
            {
                Id = Guid.NewGuid(),
                StartTime = r.StartTime,
                EndTime = r.EndTime

            }).ToList();


            await _context.SaveChangesAsync(cancellationToken);
            return new DTOs.ReminderDto
            {
                Id = reminder.Id,
                Title = reminder.Title,
                Category = reminder.Category,
                Hours = reminder.Hours,
                Date = reminder.Date,
                Ranges = reminder.Ranges.Select(r => new ReminderRangesDto
                {
                    Id = r.Id,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime
                }).ToList()
            };
        }
    }
}

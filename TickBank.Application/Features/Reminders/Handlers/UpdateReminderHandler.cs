using MediatR;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TickBank.Application.Features.ReminderRanges.DTOs;
using TickBank.Application.Features.Reminders.Commands;
using TickBank.Application.Features.Reminders.DTOs;
using TickBank.Infrastructure.Persistence.Database;
using TickBank.Domain.Entities;

namespace TickBank.Application.Features.Reminders.Handlers
{
    public class UpdateReminderHandler : IRequestHandler<UpdateReminderCommand, DTOs.ReminderDto>
    {
        private readonly ApplicationDbContext _context;

        public UpdateReminderHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReminderDto> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
        {
            var reminder = await _context.Reminders
                .Include(r => r.Ranges)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (reminder == null)
                throw new Exception("Reminder not found");

            reminder.Title = request.Title;
            reminder.Category = request.Category;
            reminder.Hours = request.Hours;
            reminder.Date = request.Date!.Value;

            _context.Ranges.RemoveRange(reminder.Ranges);

            reminder.Ranges = request.Ranges.Select(x => new ReminderRange
            {
                Id = x.Id == Guid.Empty ? Guid.NewGuid() : x.Id,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                ReminderId = reminder.Id
            }).ToList();

            await _context.SaveChangesAsync(cancellationToken);
            return new ReminderDto
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

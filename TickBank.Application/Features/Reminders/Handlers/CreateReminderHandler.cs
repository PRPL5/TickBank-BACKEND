using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TickBank.Application.Features.ReminderRanges.DTOs;
using TickBank.Application.Features.Reminders.Commands;
using TickBank.Application.Features.Reminders.DTOs;
using TickBank.Domain.Entities;
using TickBank.Infrastructure.Persistence.Database;

namespace TickBank.Application.Features.Reminders.Handlers
{
    public class CreateReminderHandler : IRequestHandler<CreateReminderCommand, ReminderDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<CreateReminderCommand> _validator;

        public CreateReminderHandler(ApplicationDbContext context, IValidator<CreateReminderCommand> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<ReminderDto> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var reminder = new Reminder
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Category = request.Category,
                Hours = request.Hours,
                Date = request.Date!.Value,
                Ranges = request.Ranges.Select(r => new ReminderRange
                {
                    Id = Guid.NewGuid(),
                    StartTime = r.StartTime,
                    EndTime = r.EndTime
                }).ToList()
            };
            

            await _context.Reminders.AddAsync(reminder, cancellationToken);
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

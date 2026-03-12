using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using TickBank.Domain.Entities;

namespace TickBank.Application.Features.Reminders.Commands
{


    public class CreateReminderValidator : AbstractValidator<CreateReminderCommand>
    {
        public CreateReminderValidator()
        {
            RuleFor(r => r.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(r => r.Category).NotEmpty().WithMessage("Category is required.");
            RuleFor(r => r.Hours).GreaterThanOrEqualTo(0).WithMessage("Hours must be a non-negative integer.");
            RuleFor(r => r.Date).NotNull().WithMessage("Date is required.");
            RuleFor(r => r.Ranges).NotNull().NotEmpty().WithMessage("Reminder range is required.");
            RuleForEach(r => r.Ranges!).ChildRules(range =>
            {
                range.RuleFor(x => x.StartTime)
                     .NotEmpty()
                     .WithMessage("StartTime is required.");

                range.RuleFor(x => x.EndTime)
                     .NotEmpty()
                     .WithMessage("EndTime is required.");
            });
        }

    }
}

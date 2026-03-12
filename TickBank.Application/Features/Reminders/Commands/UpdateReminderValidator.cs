using FluentValidation;

namespace TickBank.Application.Features.Reminders.Commands
{
    public class UpdateReminderValidator : AbstractValidator<UpdateReminderCommand>
    {
        public UpdateReminderValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(r => r.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(r => r.Category).NotEmpty().WithMessage("Category is required.");
            RuleFor(r => r.Hours).GreaterThanOrEqualTo(0).WithMessage("Hours must be a non-negative integer.");
            RuleFor(r => r.Date).NotNull().WithMessage("Date is required.");
        }
    }
}

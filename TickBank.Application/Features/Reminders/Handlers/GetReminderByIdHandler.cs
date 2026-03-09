using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickBank.Application.Features.Reminders.Queries;
using TickBank.Infrastructure.Persistence.Database;

namespace TickBank.Application.Features.Reminders.Handlers
{
    public class GetReminderByIdHandler : IRequestHandler<Queries.GetReminderByIdQuery , DTOs.ReminderDto>
    {
        private readonly ApplicationDbContext _context;

        public GetReminderByIdHandler(ApplicationDbContext context)
        {

            _context = context;
        }
        
        public async Task<DTOs.ReminderDto> Handle(GetReminderByIdQuery request, CancellationToken cancellationToken)
        {
            var reminder = await _context.Reminders.FindAsync(request.id);
            if (reminder == null)
            {
                return null;
            }
            return new DTOs.ReminderDto
            {
                Id = reminder.Id,
                Title = reminder.Title,
                Category = reminder.Category,
                Date = reminder.Date,
                Hours = reminder.Hours
            };
        }


    }
}

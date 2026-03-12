using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickBank.Application.Features.ReminderRanges.DTOs;

namespace TickBank.Application.Features.Reminders.Commands
{
    public class UpdateReminderCommand : IRequest<DTOs.ReminderDto>
    {

        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Category { get; set; } = default!;
        public int Hours { get; set; }
        public DateTime? Date { get; set; }
        public IEnumerable<ReminderRangesDto> Ranges { get; set; } = new List<ReminderRangesDto>();

    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickBank.Application.Features.Reminders.DTOs;

namespace TickBank.Application.Features.Reminders.Queries
{
    public class GetReminderByIdQuery : IRequest<ReminderDto>
    {
        public Guid id { get; }

        public GetReminderByIdQuery(Guid Id)
        {
            Id= id;

        }

    }
}

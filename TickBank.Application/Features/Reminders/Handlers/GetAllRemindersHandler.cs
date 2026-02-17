using MediatR;
using Microsoft.EntityFrameworkCore;
using TickBank.Application.Features.Reminders.Queries;
using TickBank.Application.Features.ReminderRanges.DTOs;

using TickBank.Application.Features.Reminders.DTOs;
using TickBank.Domain.Entities;
using TickBank.Infrastructure.Persistence.Database;

namespace TickBank.Application.Features.Reminders.Handlers;

public class GetAllRemindersHandler : IRequestHandler<GetAllRemindersQuery,List<ReminderDto>>
{
    private readonly ApplicationDbContext _context;
    
    public GetAllRemindersHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<ReminderDto>> Handle(GetAllRemindersQuery request, CancellationToken cancellationToken)
    {
        var allReminders = await _context.Reminders
            .Include(x => x.Ranges)
            .Select(x => new ReminderDto
            {
                Id = x.Id,
                Category = x.Category,
                Date = x.Date,
                Hours = x.Hours,
                Title = x.Title,
                Ranges = x.Ranges.Select(r => new ReminderRangesDto
                {
                    Id = r.Id,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime
                }).ToList()
            }).ToListAsync(cancellationToken);
        
        return allReminders;
    }
}
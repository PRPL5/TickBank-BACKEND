using MediatR;
using TickBank.Application.Features.Reminders.DTOs;
namespace TickBank.Application.Features.Reminders.Queries;

public class GetAllRemindersQuery:IRequest<List<ReminderDto>>
{
    
}
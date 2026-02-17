using TickBank.Application.Features.ReminderRanges.DTOs;
using TickBank.Domain.Entities;

namespace TickBank.Application.Features.Reminders.DTOs;

public class ReminderDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Category { get; set; }
    public int? Hours { get; set; }
    public DateTime? Date { get; set; }
    public IEnumerable<ReminderRangesDto> Ranges { get; set; }



}
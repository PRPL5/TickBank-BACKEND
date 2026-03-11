
namespace TickBank.Application.Features.ReminderRanges.DTOs;

public class ReminderRangesDto
{
    public Guid Id { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public Guid ReminderId { get; set; }
}
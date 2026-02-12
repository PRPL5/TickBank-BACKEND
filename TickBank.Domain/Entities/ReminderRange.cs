using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TickBank.Domain.Entities
{
    public class ReminderRange
    {
        public Guid Id { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }

        // FK + navigation property
        public Guid ReminderId { get; set; }
        public Reminder? Reminder { get; set; }
    }
}

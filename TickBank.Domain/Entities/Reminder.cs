using System;
using System.Collections.Generic;
        
namespace TickBank.Domain.Entities
{
    public class Reminder
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public int? Hours { get; set; }
        public DateTime? Date { get; set; }

        // Navigation collection expected by ApplicationDbContext
        public ICollection<ReminderRange> Ranges { get; set; } = new List<ReminderRange>();
    }
}

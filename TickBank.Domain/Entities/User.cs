using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickBank.Domain.Entities
{
    public class User {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public bool? isDeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set;} = DateTime.Now;
        public ICollection<Reminder> Reminders { get; set; }

    
    }
}

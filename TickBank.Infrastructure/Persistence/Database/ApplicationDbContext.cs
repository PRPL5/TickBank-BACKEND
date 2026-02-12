using Microsoft.EntityFrameworkCore;
using TickBank.Domain.Entities;

namespace TickBank.Infrastructure.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ReminderRange> Ranges { get; set; } = null!;
        public DbSet<Reminder> Reminders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure EF understands the one-to-many relationship
            modelBuilder.Entity<Reminder>()
                .HasMany(r => r.Ranges)
                .WithOne(rr => rr.Reminder)
                .HasForeignKey(rr => rr.ReminderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
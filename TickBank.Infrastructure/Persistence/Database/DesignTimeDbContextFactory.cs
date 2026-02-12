using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TickBank.Infrastructure.Persistence.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // Adjust connection string for your dev machine if needed
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TickBankDB;Trusted_Connection=True;");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
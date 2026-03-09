using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TickBank.Infrastructure.Persistence.Database
{
    //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    //{
    //    public ApplicationDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
    //        var configuration = new ConfigurationBuilder()
    //            .SetBasePath(Path.GetFullPath("../TickBank.Web"))
    //            .AddJsonFile("appsettings.json")
    //            .Build();
            
    //        var connectionString = configuration.GetConnectionString("DefaultConnection");
    //        optionsBuilder.UseNpgsql(connectionString);
    //        return new ApplicationDbContext(optionsBuilder.Options);
    //    }
    //}
}
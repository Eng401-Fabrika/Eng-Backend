using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Eng_Backend.DAL.DbContext
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
           
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), ".."));

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("Eng-BackendAPI/appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
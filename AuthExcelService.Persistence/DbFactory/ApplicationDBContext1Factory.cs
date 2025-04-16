using AuthExcelService.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AuthExcelService.Persistence.DbFactory
{
    public class ApplicationDBContext1Factory : IDesignTimeDbContextFactory<ApplicationDBContext1>
    {
        public ApplicationDBContext1 CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../AuthExcelService.API");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)  // Explicitly set path to API project
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("ApplicationDB1");

            var builder = new DbContextOptionsBuilder<ApplicationDBContext1>();
            builder.UseSqlServer(connectionString);

            return new ApplicationDBContext1(builder.Options);
        }
    }
}

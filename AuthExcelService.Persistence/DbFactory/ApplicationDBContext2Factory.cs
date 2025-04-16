using AuthExcelService.Persistence.Data;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Persistence.DbFactory
{
    public class ApplicationDBContext2Factory : IDesignTimeDbContextFactory<ApplicationDBContext2>
    {
        public ApplicationDBContext2 CreateDbContext(string[] args)
        {

            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../AuthExcelService.API");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)  // Explicitly set path to API project
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("ApplicationDB2");

            var builder = new DbContextOptionsBuilder<ApplicationDBContext2>();
            builder.UseSqlServer(connectionString);

            return new ApplicationDBContext2(builder.Options);
        }
    }
}

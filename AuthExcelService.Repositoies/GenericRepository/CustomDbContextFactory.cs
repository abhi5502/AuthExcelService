using AuthExcelService.Persistence.Data;
using AuthExcelService.Repositoies.IGenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Repositoies.GenericRepository
{
    public class CustomDbContextFactory : ICustomDbContextFactory
    {
        private readonly IServiceProvider _provider;
        public CustomDbContextFactory(IServiceProvider provider)
        {
            _provider = provider;
        }
        public DbContext GetDbContextByCountry(string country)
        {
            return country switch
            {
                "India" => _provider.GetRequiredService<ApplicationDBContext1>(),
                "UK" => _provider.GetRequiredService<ApplicationDBContext2>(),
                "US" => _provider.GetRequiredService<ApplicationDBContext3>(),
                _ => throw new Exception("Invalid Country")
            };
        }
    }
}

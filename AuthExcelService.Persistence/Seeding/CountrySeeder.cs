using AuthExcelService.Domain.Entities;
using AuthExcelService.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Persistence.Seeding
{
    public static class CountrySeeder
    {
        public static async Task SeedCountryAsync(ApplicationDBContext1 context)
        {
            if (!context.Countries.Any())
            {
                var countries = new List<Country>
                {
                   new Country
                   {
                       //Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                       Id=Guid.NewGuid(),
                       CountryName = "India"
                   },
                   new Country
                   {
                       //Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        Id=Guid.NewGuid(),
                       CountryName = "UK"
                   },
                   new Country
                   {
                       //Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                        Id=Guid.NewGuid(),
                       CountryName = "US"
                   }

                };

                await context.Countries.AddRangeAsync(countries);
                await context.SaveChangesAsync();
            }
        }
    }
}

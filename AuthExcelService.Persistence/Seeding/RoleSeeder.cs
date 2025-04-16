using AuthExcelService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Persistence.Seeding
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            string[] roleNames = { "Admin", "Staff", "User" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                }
            }
        }
    }
}

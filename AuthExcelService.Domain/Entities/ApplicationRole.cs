using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
        //public ICollection<RoleCountry> RoleCountries { get; set; } = new List<RoleCountry>();
        //public ICollection<UserCountry> UserCountries { get; set; } = new List<UserCountry>();

    }
}

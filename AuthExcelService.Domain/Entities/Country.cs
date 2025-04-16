using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Entities
{
    public class Country
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; } = string.Empty;

        // Navigation property: One country can be linked to many UserCountry entries
        //public ICollection<RoleCountry> RoleCountries { get; set; } = new List<RoleCountry>();

        //public ICollection<UserCountry> UserCountries { get; set; } = new List<UserCountry>();

        public UserCountry UserCountries { get; set; } = null!;


    }

}

using AuthExcelService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Dtos.Auth
{
    public class CountryDto
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; }

       
        //public ICollection<UserCountry> UserCountries { get; set; } = new List<UserCountry>();
        public UserCountry UserCountries { get; set; } = null!;
    }
}

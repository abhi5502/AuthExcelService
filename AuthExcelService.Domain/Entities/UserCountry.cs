using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Entities
{
    public class UserCountry
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid CountryId { get; set; }
        public Country Country { get; set; }
    }
}

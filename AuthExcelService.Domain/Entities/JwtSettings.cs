using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Entities
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Key { get; set; } = null!;
        public int ExpirationInMinutes { get; set; }
    }
}

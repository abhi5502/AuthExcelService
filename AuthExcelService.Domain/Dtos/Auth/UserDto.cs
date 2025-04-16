using AuthExcelService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Dtos.Auth
{
    public class UserDto
    {
        public string ID { get; set; }=null!;
        public string? UserName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public RoleDto Roles { get; set; } = null!;

        public CountryDto Countries { get; set; } = null!;
    }


}

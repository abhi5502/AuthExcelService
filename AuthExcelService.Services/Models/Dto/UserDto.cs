using AuthExcelService.Domain.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Services.Models.Dto
{
    public class UserDto
    {
        public string ID { get; set; } = null!;
        public string? UserName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public RoleDto Roles { get; set; } = null!;
        public CountryDto Countries { get; set; } = null!;
    }
}

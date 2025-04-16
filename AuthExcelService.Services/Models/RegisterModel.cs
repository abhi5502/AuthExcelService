using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Services.Models
{
  
    public class RegisterModel
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; } = null!;
        public string? ConfirmPassword { get; set; }
        public string Role { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}

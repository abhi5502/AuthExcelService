using AuthExcelService.Services.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Services.Models.ResponseModel
{
    public class LoginResponseWebDto
    {
        public UserDto? User { get; set; }
        public string? Token { get; set; }

       
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Dtos.Auth
{
    public class LoginResponseDto
    {
        public UserDto?  User { get; set; }
        public string? Token { get; set; }
    }
}

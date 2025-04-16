using AuthExcelService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Repositoies.Contracts.ITokenService
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(ApplicationUser user, IList<string> roles);
    }
}

using AuthExcelService.Repositoies.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Repositoies.Contracts
{
    public interface IAdminRepository<T> : IGenericRepository<T> where T : class
    {
    }
}

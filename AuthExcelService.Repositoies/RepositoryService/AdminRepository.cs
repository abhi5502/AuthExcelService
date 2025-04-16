using AuthExcelService.Repositoies.Contracts;
using AuthExcelService.Repositoies.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Repositoies.RepositoryService
{
    public class AdminRepository<T> : GenericRepository<T>, IAdminRepository<T> where T : class
    {
        public AdminRepository(DbContext dbContext) : base(dbContext)
        {
        }

    }
}

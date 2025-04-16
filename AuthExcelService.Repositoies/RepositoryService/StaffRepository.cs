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
    public class StaffRepository<T> : GenericRepository<T>, IStaffRepository<T> where T : class
    {
        public StaffRepository(DbContext dbContext) : base(dbContext)
        {
                
        }
    }
}

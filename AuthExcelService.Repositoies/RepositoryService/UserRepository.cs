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
    public class UserRepository<T> : GenericRepository<T>, IUserRepository<T> where T : class
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            
        }
    }
}

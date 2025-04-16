using AuthExcelService.Domain.Entities.FormatA;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Persistence.Data
{
    public class ApplicationDBContext2 : DbContext
    {
        public ApplicationDBContext2(DbContextOptions<ApplicationDBContext2> options)
            : base(options)
        {
        }

        public DbSet<FormatAFile> FormatBFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


        }
    }
}

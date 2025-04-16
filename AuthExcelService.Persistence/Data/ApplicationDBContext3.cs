using AuthExcelService.Domain.Entities.FormatA;
using Microsoft.EntityFrameworkCore;
namespace AuthExcelService.Persistence.Data
{
    public class ApplicationDBContext3 : DbContext
    {
        public ApplicationDBContext3(DbContextOptions<ApplicationDBContext3> options)
            : base(options)
        {
        }

        public DbSet<FormatAFile> FormatCFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


        }
    }
}

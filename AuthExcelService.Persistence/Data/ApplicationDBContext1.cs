using AuthExcelService.Domain.Entities;
using AuthExcelService.Domain.Entities.FormatA;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AuthExcelService.Persistence.Data
{
    public class ApplicationDBContext1 : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDBContext1(DbContextOptions<ApplicationDBContext1> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<FormatAFile> FormatAFiles { get; set; }

        // Add DbSets for RoleCountry and Country
        //public DbSet<RoleCountry> RoleCountries { get; set; }
        public DbSet<UserCountry> UserCountries { get; set; }
        public DbSet<Country> Countries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<RoleCountry>()
            //    .HasKey(rc => new { rc.RoleId, rc.CountryId });

            //modelBuilder.Entity<RoleCountry>()
            //    .HasOne(rc => rc.Role)
            //    .WithMany(r => r.RoleCountries)
            //    .HasForeignKey(rc => rc.RoleId);

            //modelBuilder.Entity<RoleCountry>()
            //    .HasOne(rc => rc.Country)
            //    .WithMany(c => c.RoleCountries)
            //    .HasForeignKey(rc => rc.CountryId);



            modelBuilder.Entity<UserCountry>()
        .       HasKey(uc => new { uc.UserId, uc.CountryId });

            modelBuilder.Entity<UserCountry>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCountries)
                .HasForeignKey(uc => uc.UserId);

            //modelBuilder.Entity<UserCountry>()
            //    .HasOne(uc => uc.Country)
            //    .WithMany(c => c.UserCountries)
            //    .HasForeignKey(uc => uc.CountryId);


        }

    }
}

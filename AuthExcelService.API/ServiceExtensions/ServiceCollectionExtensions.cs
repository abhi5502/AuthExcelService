using AuthExcelService.API.MappingProfile;
using AuthExcelService.API.Models;
using AuthExcelService.Domain.Entities;
using AuthExcelService.Persistence.Data;
using AuthExcelService.Repositoies.Contracts;
using AuthExcelService.Repositoies.Contracts.IEmaiService;
using AuthExcelService.Repositoies.Contracts.ITokenService;
using AuthExcelService.Repositoies.RepositoryService;
using AuthExcelService.Repositoies.RepositoryService.EmailService;
using AuthExcelService.Repositoies.RepositoryService.TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AuthExcelService.API.ServiceExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient("EXlAPI", client =>
            {
                client.Timeout = TimeSpan.FromMinutes(30); // Set timeout to 120 seconds
            });


            services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*().~?></|{}[]";
                options.User.RequireUniqueEmail = true;
            });





            // Add AutoMapper
            services.AddAutoMapper(typeof(MappingConfig));

            // Add DbContext with SQL Server
            // 1st Database
            services.AddDbContext<ApplicationDBContext1>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ApplicationDB1")));
            // 2nd Database
            services.AddDbContext<ApplicationDBContext2>(options =>
               options.UseSqlServer(configuration.GetConnectionString("ApplicationDB2")));
            // 3rd Database
            services.AddDbContext<ApplicationDBContext3>(options =>
               options.UseSqlServer(configuration.GetConnectionString("ApplicationDB3")));

            // Configure Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDBContext1>()
                .AddDefaultTokenProviders();




            // Add authentication and authorization
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(jwtSettings.Key);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    RoleClaimType = ClaimTypes.Role
                };
            });








            // Register other services
            services.AddScoped<ApiResponse>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}

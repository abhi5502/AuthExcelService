using AuthExcelService.Services.IRepository;
using AuthExcelService.Services.Repository;
using AuthExcelService.WebApp.WebMappingProfile;

namespace AuthExcelService.WebApp.WebExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebAppServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthWebService, AuthWebService>(client =>
            {
                client.Timeout = TimeSpan.FromMinutes(5); // Set timeout to 120 seconds
            });
            services.AddScoped<IAuthWebService, AuthWebService>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            //services.AddScoped<IAuthExcelServices, AuthExcelServices>();
            services.AddScoped<IBaseService, BaseService>();

            services.AddAutoMapper(typeof(MappingProfile));

           




            // Add more services here as needed
            return services;
        }
    }
}

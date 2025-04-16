using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace AuthExcelService.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration, Assembly applicationAssembly, string appName)
        {
            // Add Serilog logging
            services.AddSerilogLogging(configuration, applicationAssembly, appName);
            services.AddLogging().AddSerilog();

            return services;
        }

    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
//using Serilog.Enrichers.WithCaller;
using Serilog.Events;
using Serilog.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Common.Extensions
{
    public static class SerilogExtensions
    {
        public static void AddSerilogLogging(this IServiceCollection services, IConfiguration configuration, Assembly applicationAssembly, string appName)
        {
            var logDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../AuthExcelService.Common/Logs"));
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            var logFileName = $"{appName}-log-.txt";

            // Configure Serilog
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Information()
            //    .ReadFrom.Configuration(configuration)
            //    .Enrich.FromLogContext()
            //    .Enrich.WithExceptionDetails()              // for stack trace
            //    .Enrich.WithEnvironmentName()
            //    .Enrich.WithMachineName()
            //    .Enrich.WithProcessId()
            //    .Enrich.WithThreadId()
            //    .Enrich.WithCaller()                        // <-- NEW: For class/method/line info
            //    .WriteTo.Console()
            //    .WriteTo.File(
            //        path: Path.Combine(logDirectory, logFileName),
            //        rollingInterval: RollingInterval.Day,
            //        shared: true,
            //        retainedFileCountLimit: 10,
            //        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj} {Exception} {Properties:j}{NewLine}"
            //    )
            //    .CreateLogger();



            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()
                //.Enrich.WithCaller() // ✅ Correct method from Serilog.Enrichers.CallerInfo
                .WriteTo.Console()
                .WriteTo.File(
                    path: Path.Combine(logDirectory, logFileName),
                    rollingInterval: RollingInterval.Day,
                    shared: true,
                    retainedFileCountLimit: 10
                )
                .CreateLogger();


            // Optional: Add Serilog to services
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        }

    }
}

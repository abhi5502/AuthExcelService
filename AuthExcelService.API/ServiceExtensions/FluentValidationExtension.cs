using AuthExcelService.API.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace AuthExcelService.API.ServiceExtensions
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<ChangePasswordValidator>();
            services.AddValidatorsFromAssemblyContaining<ForgetPasswordValidator>();


            return services;
        }
    }
}

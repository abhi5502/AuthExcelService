using AuthExcelService.Services.Models;
using AuthExcelService.WebApp.Models.Auth;
using AutoMapper;

namespace AuthExcelService.WebApp.WebMappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginViewModel, LoginModel>();
            CreateMap<RegisterViewModel, RegisterModel>();
            // ...other mappings...
            CreateMap<ForgetPasswordViewModel, ForgetPasswordModel>();
            CreateMap<ChangePasswordViewModel, ChangePasswordModel>();

        }
    }
}

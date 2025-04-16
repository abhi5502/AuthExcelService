using AuthExcelService.API.Models.Dtos;
using AuthExcelService.Domain.Dtos;
using AuthExcelService.Domain.Dtos.Auth;
using AuthExcelService.Domain.Dtos.FileUpload;
using AuthExcelService.Domain.Dtos.FileView;
using AuthExcelService.Domain.Entities;
using AuthExcelService.Domain.Entities.FormatA;
using AuthExcelService.Domain.Entities.FormatB;
using AuthExcelService.Domain.Entities.FormatC;
using AutoMapper;

namespace AuthExcelService.API.MappingProfile
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            //CreateMap<LoginRequestDto, LoginDto>();

            CreateMap<RegisterationDto, ApplicationUser>()
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));


            // Format A
            CreateMap<FormatAFile, Format_A_UploadRequestDto>().ReverseMap();
            CreateMap<FormatAFile, Format_A_ViewResponseDto>().ReverseMap();

            // Format B
            CreateMap<FormatBFile, Format_B_UploadRequestDto>().ReverseMap();
            CreateMap<FormatBFile, Format_B_ViewResponseDto>().ReverseMap();

            // Format C
            CreateMap<FormatCFile, Format_C_UploadRequestDto>().ReverseMap();
            CreateMap<FormatCFile, Format_C_ViewResponseDto>().ReverseMap();

        }
    }
}

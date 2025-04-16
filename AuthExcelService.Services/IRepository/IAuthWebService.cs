using AuthExcelService.Services.Models;
using AuthExcelService.Services.Models.ResponseModel;

namespace AuthExcelService.Services.IRepository
{
    public interface IAuthWebService
    {
        //Task<ApiResponseWeb> LoginAsync(LoginModel loginRequest);
        //Task<ApiResponseWeb> RegisterAsync(RegisterModel registerRequest);
        Task<ApiResponseWeb> ForgetPasswordAsync(ForgetPasswordModel forgetPasswordRequest);
        Task<ApiResponseWeb> ChangePasswordAsync(ChangePasswordModel changePasswordRequest);

        //Task<ApiResponseWeb?> AssignRoleAsync(RegisterModel registrationRequestDto);


        Task<ApiResponseWeb?> LoginAsync(LoginModel loginRequestDto);
        Task<ApiResponseWeb?> RegisterAsync(RegisterModel registrationRequestDto);
        Task<ApiResponseWeb?> AssignRoleAsync(RegisterModel registrationRequestDto);

        Task<ApiResponseWeb?> CountryAssignAsync(RegisterModel registrationRequestDto);

    }
}

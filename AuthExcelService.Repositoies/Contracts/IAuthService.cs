using AuthExcelService.Domain.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Repositoies.Contracts
{
    public interface IAuthService
    {
        Task<bool> IsUniqueUser(string username);
        Task<LoginResponseDto> Login(LoginDto loginRequestDTO);
        //Task<UserDto> Register(RegisterationDto registerationRequestDTO);
        Task<bool> ForgetPasswordAsync(string email);
        Task<bool> ChangePasswordAsync(string userName, string currentPassword, string newPassword);

        //Task<bool> AssignRole(string email, string roleName);

        Task<UserDto> Register(RegisterationDto registrationRequestDto);

        Task<bool> AssignRolesAsync(string email, List<string> roleNames);

        Task<UserDto?> GetUserByEmailAsync(string email);
        Task SavePasswordResetTokenAsync(string userId, string resetToken);


        Task<bool> AssignCountrytoUserAsync(string email, List<string> countryNames);


    }
}

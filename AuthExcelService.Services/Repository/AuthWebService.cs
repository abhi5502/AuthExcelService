using AuthExcelService.Domain.Dtos.Auth;
using AuthExcelService.Services.IRepository;
using AuthExcelService.Services.Models;
using AuthExcelService.Services.Models.Dto;
using AuthExcelService.Services.Models.RequestModel;
using AuthExcelService.Services.Models.ResponseModel;
using AuthExcelService.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

//using static AuthExcelService.Utility.StaticDetails;


namespace AuthExcelService.Services.Repository
{
    public class AuthWebService : IAuthWebService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<AuthWebService> _logger;
        public AuthWebService(IBaseService baseService, ILogger<AuthWebService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<ApiResponseWeb?> AssignRoleAsync(RegisterModel registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticDetails.AuthAPIBase + "/api/auth/assignRole"
            });
        }


        public async Task<ApiResponseWeb?> LoginAsync(LoginModel loginRequest)
        {
            try
            {
                _logger.LogInformation("Starting LoginAsync for user: {UserEmail}", loginRequest.Email);

                var response = await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = StaticDetails.ApiType.POST,
                    Data = loginRequest,
                    Url = StaticDetails.AuthAPIBase + "/api/auth/login"
                }, withBearer: false);

                // Log the API response
                _logger.LogInformation("API Response: {@responseDto}", response);

                if (response != null)
                {
                    _logger.LogInformation("LoginAsync completed for user: {UserEmail} with status: {StatusCode}",
                        loginRequest.Email, response.StatusCode);
                }
                else
                {
                    _logger.LogWarning("LoginAsync returned null response for user: {UserEmail}", loginRequest.Email);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during LoginAsync for user: {UserEmail}", loginRequest.Email);
                throw;
            }
        }



        public async Task<ApiResponseWeb?> RegisterAsync(RegisterModel registerRequest)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registerRequest,
                Url = StaticDetails.AuthAPIBase + "/api/auth/register"
            }, withBearer: false);
        }

        public async Task<ApiResponseWeb> ChangePasswordAsync(ChangePasswordModel changePasswordRequest)
        {
            var response = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = changePasswordRequest,
                Url = StaticDetails.AuthAPIBase + "/api/Auth/change-password"
            }, withBearer: false);

            return response; 
        }

        public async Task<ApiResponseWeb> ForgetPasswordAsync(ForgetPasswordModel forgetPasswordRequest)
        {
            var response= await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = forgetPasswordRequest,
                Url = StaticDetails.AuthAPIBase + "/api/auth/forget-password"
            }, withBearer: false);

            return response;
        }

        public async Task<ApiResponseWeb?> CountryAssignAsync(RegisterModel registrationRequestDto)
        {
            try
            {
                var response = await _baseService.SendAsync(new RequestDto
                {
                    ApiType = StaticDetails.ApiType.POST,
                    Data = registrationRequestDto,
                    Url = StaticDetails.AuthAPIBase + "/api/auth/assign-country-to-user"
                }, withBearer: false);

                if (response == null)
                {
                    _logger.LogWarning("Response is null.");
                    return null;
                }

                return response;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to deserialize API response. Raw response: {RawResponse}", ex.Message);
                _logger.LogError("StackTrace: {StackTrace}", ex.StackTrace);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while assigning country for user: {UserEmail}. StackTrace: {StackTrace}",
                    registrationRequestDto.Email, ex.StackTrace);
                throw;
            }
        }
    }
}

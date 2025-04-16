using AuthExcelService.API.Models;
using AuthExcelService.API.Models.Dtos;
using AuthExcelService.Domain.Dtos.Auth;
using AuthExcelService.Repositoies.Contracts;
using AuthExcelService.Repositoies.Contracts.IEmaiService;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace AuthExcelService.API.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        protected ApiResponse _response;
        protected readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Constructor for AuthController
        /// </summary>
        /// <param name="authService"></param>
        /// <param name="logger"></param>
        /// <param name="response"></param>
        /// <param name="mapper"></param>
        public AuthController(IAuthService authService, ILogger<AuthController> logger, ApiResponse response, IMapper mapper, IEmailService emailService)
        {
            _authService = authService;
            _logger = logger;
            _response = response;
            _mapper = mapper;
            _emailService = emailService;
        }


        /// <summary>
        /// Login existing user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogWarning("Validation failed for login request");

        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        return BadRequest(_response);
        //    }

        //    try
        //    {
        //        // Use AutoMapper to map LoginRequestDto to LoginRequestDTO
        //        var loginRequestDTO = _mapper.Map<LoginDto>(model);
        //        var loginResponse = await _authService.Login(loginRequestDTO);

        //        if (loginResponse == null)
        //        {
        //            _logger.LogWarning("Login response is null for user: {Email}", model.Email);
        //            _response.StatusCode = HttpStatusCode.InternalServerError;
        //            _response.IsSuccess = false;
        //            _response.ErrorMessages.Add("An error occurred while processing your request.");
        //            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        //        }

        //        if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
        //        {
        //            _logger.LogWarning("Login failed for user: {Email}. Invalid username or password.", model.Email);
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            _response.IsSuccess = false;
        //            _response.ErrorMessages.Add("Username or password is incorrect");
        //            return BadRequest(_response);
        //        }

        //        _logger.LogInformation("User {Email} logged in successfully.", model.Email);
        //        _response.StatusCode = HttpStatusCode.OK;
        //        _response.IsSuccess = true;
        //        _response.Result = loginResponse;
        //        return Ok(_response);
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        _logger.LogError(ex, "Null reference encountered while processing login for user: {Email}", model.Email);
        //        _response.StatusCode = HttpStatusCode.InternalServerError;
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages.Add("An unexpected error occurred. Please try again later.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, _response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while processing the login request for user: {Email}", model.Email);
        //        _response.StatusCode = HttpStatusCode.InternalServerError;
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages.Add("An error occurred while processing your request.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, _response);
        //    }
        //}

        /// <summary>
        /// Register a new user like admin,staff,user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationDto model)
        {
            var user = await _authService.Register(model);
            if (user == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Registration failed. Please try again." };
                return BadRequest(_response);
            }

            _response.IsSuccess = true;
            _response.Result = user;
            return Ok(_response);
        }

        /// <summary>
        /// forget password for existing user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid email format for forget password request: {Email}", model.Email);
                    return BadRequest(new { Message = "Invalid email format." });
                }

                _logger.LogInformation("Forget password request received for email: {Email}", model.Email);

                // Check if email exists in the database
                var user = await _authService.GetUserByEmailAsync(model.Email); // Replace with your repository method
                if (user == null)
                {
                    _logger.LogWarning("Email not found for forget password request: {Email}", model.Email);
                    return NotFound(new { Message = "Email not found." });
                }

                // Generate reset token
                var resetToken = Guid.NewGuid().ToString();
                await _authService.SavePasswordResetTokenAsync(user.ID, resetToken); // Replace with your repository method

                // Create the reset link
                var resetLink = $"{Request.Scheme}://{Request.Host}/Auth/ResetPassword?token={resetToken}";

                // Send email with reset link
                await _emailService.SendEmailAsync(model.Email, "Reset Your Password", $"Click <a href='{resetLink}'>here</a> to reset your password.");

                _logger.LogInformation("Password reset instructions sent successfully to email: {Email}", model.Email);

                return Ok(new { Message = "Password reset instructions have been sent to your email." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the forget password request for email: {Email}", model.Email);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while processing your request." });
            }
        }


        /// <summary>
        /// Reset Password for existing user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            try
            {
                _logger.LogInformation("Change password request received for user ID: {Email}", model.Email);

                if (model.NewPassword != model.ConfirmPassword)
                {
                    _logger.LogWarning("Password confirmation does not match for user ID: {Email}", model.Email);

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("New password and confirmation password do not match.");
                    return BadRequest(_response);
                }

                var result = await _authService.ChangePasswordAsync(model.Email, model.CurrentPassword, model.NewPassword);

                if (!result)
                {
                    _logger.LogWarning("Password change failed for user ID: {Email}", model.Email);

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Password change failed. Please check your credentials.");
                    return BadRequest(_response);
                }

                _logger.LogInformation("Password changed successfully for user ID: {Email}", model.Email);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = new { message = "Password changed successfully." };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the change password request for user ID: {Email}", model.Email);

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("An error occurred while processing your request.");
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }


        /// <summary>
        ///  Assgin a role to user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssginRoleDto model)
        {
            try
            {
                _logger.LogInformation("Assign role request received for email: {Email} with role: {Role}", model.Email, model.Role);

                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Role))
                {
                    _logger.LogWarning("Invalid input: Email or Role is null or empty.");
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Email and Role are required." };
                    return BadRequest(_response);
                }

                var roleList = new List<string> { model.Role.ToUpper() };
                var assignRoleSuccessful = await _authService.AssignRolesAsync(model.Email, roleList);

                if (!assignRoleSuccessful)
                {
                    _logger.LogWarning("Failed to assign role {Role} to email: {Email}", model.Role, model.Email);
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Error encountered while assigning role." };
                    return BadRequest(_response);
                }

                _logger.LogInformation("Role {Role} assigned successfully to email: {Email}", model.Role, model.Email);
                _response.IsSuccess = true;
                _response.Result = new { message = "Role assigned successfully." };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while assigning role {Role} to email: {Email}", model.Role, model.Email);
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "An error occurred while processing your request." };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var loginResponse = await _authService.Login(_mapper.Map<LoginDto>(model));
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);

        }


        //[HttpPost("assign-country-to-user")]
        //public async Task<IActionResult> AssignCountryUser([FromBody] AssginCountryDto model)
        //{
        //    if (string.IsNullOrWhiteSpace(model.Email) || model.Countries == null || !model.Countries.Any())
        //        return BadRequest("Email and at least one country name must be provided.");

        //    var result = await _authService.AssignCountrytoUserAsync(model.Email, model.Countries);

        //    if (!result)
        //        return NotFound("User not found or failed to assign countries.");

        //    return Ok("Countries assigned to user successfully.");
        //}


        [HttpPost("assign-country-to-user")]
        public async Task<IActionResult> AssignCountryUser([FromBody] AssginCountryDto model)
        {
            try
            {
                _logger.LogInformation("Assign country request received for email: {Email} with country: {Country}", model.Email, model.Country);

                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Country))
                {
                    _logger.LogWarning("Invalid input: Email or Country is null or empty.");
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Email and Country are required." };
                    return BadRequest(_response);
                }

                // Convert single country string into a list
                var countryList = new List<string> { model.Country };

                var assignCountrySuccessful = await _authService.AssignCountrytoUserAsync(model.Email, countryList);

                if (!assignCountrySuccessful)
                {
                    _logger.LogWarning("Failed to assign country {Country} to email: {Email}", model.Country, model.Email);
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Error encountered while assigning country." };
                    return BadRequest(_response);
                }

                _logger.LogInformation("Country {Country} assigned successfully to email: {Email}", model.Country, model.Email);
                _response.IsSuccess = true;
                _response.Result = new { message = "Country assigned successfully." };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while assigning country {Country} to email: {Email}", model.Country, model.Email);
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "An error occurred while processing your request." };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }



    }
}

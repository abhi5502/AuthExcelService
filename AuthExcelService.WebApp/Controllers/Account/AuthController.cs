using AuthExcelService.Domain.Dtos.Auth;
using AuthExcelService.Services.IRepository;
using AuthExcelService.Services.Models;
using AuthExcelService.Services.Models.ResponseModel;
using AuthExcelService.Utility;
using AuthExcelService.WebApp.Models.Auth;
using AuthExcelService.WebApp.Models.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthExcelService.WebApp.Controllers.Account
{
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;
        public IAuthWebService _authWebService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IMapper mapper, ILogger<AuthController> logger, IAuthWebService authWebService, ITokenProvider tokenProvider)
        {
            _mapper = mapper;
            _logger = logger;
            _authWebService = authWebService;
            _tokenProvider = tokenProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem { Text = StaticDetails.SessionRoleAdmin, Value = StaticDetails.SessionRoleAdmin },
                new SelectListItem { Text = StaticDetails.SessionRoleStaff, Value = StaticDetails.SessionRoleStaff },
                new SelectListItem { Text = StaticDetails.SessionRoleUser, Value = StaticDetails.SessionRoleUser }
            };
            ViewBag.RoleList = roleList;

            var countryList = new List<SelectListItem>()
            {
                new SelectListItem { Text = StaticDetails.CountryIndia, Value = StaticDetails.CountryIndia },
                new SelectListItem { Text = StaticDetails.CountryUS, Value = StaticDetails.CountryUS },
                new SelectListItem { Text = StaticDetails.CountryUK, Value = StaticDetails.CountryUK }
            };
            ViewBag.CountryList = countryList;

            return View();
        }


        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel obj)
        //{
        //    ApiResponseWeb result = await _authWebService.RegisterAsync(_mapper.Map<RegisterModel>(obj));
        //    ApiResponseWeb assignRole;
        //    ApiResponseWeb assignCountry;

        //    //if (result != null && result.IsSuccess)
        //    //{
        //    //    if (string.IsNullOrEmpty(obj.Role))
        //    //    {
        //    //        obj.Role = StaticDetails.SessionRoleUser;
        //    //    }

        //    //    // Assign role to the user
        //    //    assingRole = await _authWebService.AssignRoleAsync(_mapper.Map<RegisterModel>(obj));
        //    //    _logger.LogError("Assigning role to user {Email}.", obj.Email);



        //    //    if (string.IsNullOrWhiteSpace(obj.Country))
        //    //    {
        //    //        _logger.LogWarning("Country is missing for user {Email}.", obj.Email);
        //    //        TempData["error"] = "Country is required.";
        //    //        return View(obj);
        //    //    }

        //    //    // Assign country to the user

        //    //    _logger.LogWarning("Assigning country to user {Email}.", obj.Email);
        //    //    assignCountry = await _authWebService.CountryAssignAsync(_mapper.Map<RegisterModel>(obj));


        //    //    if (assignCountry != null && assignCountry.IsSuccess)
        //    //    {
        //    //        TempData["success"] = "Registration Successful";
        //    //        return RedirectToAction(nameof(Login));
        //    //    }
        //    //    else
        //    //    {
        //    //        _logger.LogError("Assign country failed for user {Email}.", obj.Email);
        //    //        TempData["error"] = assignCountry?.ErrorMessages;
        //    //        _logger.LogError("Assigning Role failed for user {Email}.", obj.Country);
        //    //        TempData["error"] = assignCountry?.ErrorMessages;
        //    //    }


        //    //}
        //    //else
        //    //{
        //    //    _logger.LogError("Registration failed for user {Email}.", obj.Email);
        //    //    TempData["error"] = result?.ErrorMessages;
        //    //}


        //    if (result != null && result.IsSuccess)
        //    {
        //        // Ensure role is set, defaulting if not provided
        //        if (string.IsNullOrEmpty(obj.Role))
        //        {
        //            obj.Role = StaticDetails.SessionRoleUser;
        //        }

        //        // Assign role to the user
        //        assignRole = await _authWebService.AssignRoleAsync(_mapper.Map<RegisterModel>(obj));
        //        if (assignRole == null || !assignRole.IsSuccess)
        //        {
        //            _logger.LogError("Assigning role failed for user {Email}.", obj.Email);
        //            TempData["error"] = assignRole?.ErrorMessages != null && assignRole.ErrorMessages.Any() ? string.Join(", ", assignRole.ErrorMessages) : "Role assignment failed.";
        //            return View(obj);
        //        }

        //        // Check if country is provided
        //        if (string.IsNullOrWhiteSpace(obj.Country))
        //        {
        //            _logger.LogWarning("Country is missing for user {Email}.", obj.Email);
        //            TempData["error"] = "Country is required.";
        //            return View(obj);
        //        }

        //        // Assign country to the user
        //        _logger.LogInformation("Assigning country to user {Email}.", obj.Email);
        //         assignCountry = await _authWebService.CountryAssignAsync(_mapper.Map<RegisterModel>(obj));

        //        if (assignCountry != null && assignCountry.IsSuccess)
        //        {
        //            TempData["success"] = "Registration Successful";
        //            return RedirectToAction(nameof(Login));
        //        }
        //        else
        //        {
        //            _logger.LogError("Assign country failed for user {Email}.", obj.Email);
        //            TempData["error"] = assignCountry?.ErrorMessages != null && assignCountry.ErrorMessages.Any()
        //                ? string.Join(", ", assignCountry.ErrorMessages)
        //                : "Country assignment failed.";
        //            return View(obj);
        //        }
        //    }
        //    else
        //    {
        //        _logger.LogError("Registration failed for user {Email}.", obj.Email);
        //        TempData["error"] = result?.ErrorMessages != null && result.ErrorMessages.Any()
        //           ? string.Join(", ", result.ErrorMessages)
        //           : "Registration failed.";
        //        return View(obj);
        //    }



        //    var roleList = new List<SelectListItem>()
        //    {
        //        new SelectListItem{Text=StaticDetails.SessionRoleAdmin,Value=StaticDetails.SessionRoleAdmin},
        //        new SelectListItem{Text=StaticDetails.SessionRoleStaff,Value=StaticDetails.SessionRoleAdmin},
        //        new SelectListItem{Text=StaticDetails.SessionRoleUser,Value=StaticDetails.SessionRoleUser},
        //    };
        //    ViewBag.RoleList = roleList;

        //    var countryList = new List<SelectListItem>()
        //    {
        //        new SelectListItem { Text = StaticDetails.CountryIndia, Value = StaticDetails.CountryIndia },
        //        new SelectListItem { Text = StaticDetails.CountryUS, Value = StaticDetails.CountryUS },
        //        new SelectListItem { Text = StaticDetails.CountryUK, Value = StaticDetails.CountryUK }
        //    };
        //    ViewBag.CountryList = countryList;


        //    return View(obj);
        //}


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel obj)
        {
            
                void PopulateDropdowns()
                {
                    ViewBag.RoleList = new List<SelectListItem>
                        {
                            new SelectListItem { Text = StaticDetails.SessionRoleAdmin, Value = StaticDetails.SessionRoleAdmin },
                            new SelectListItem { Text = StaticDetails.SessionRoleStaff, Value = StaticDetails.SessionRoleStaff },
                            new SelectListItem { Text = StaticDetails.SessionRoleUser, Value = StaticDetails.SessionRoleUser }
                        };

                    ViewBag.CountryList = new List<SelectListItem>
                        {
                            new SelectListItem { Text = StaticDetails.CountryIndia, Value = StaticDetails.CountryIndia },
                            new SelectListItem { Text = StaticDetails.CountryUS, Value = StaticDetails.CountryUS },
                            new SelectListItem { Text = StaticDetails.CountryUK, Value = StaticDetails.CountryUK }
                        };
                }

            PopulateDropdowns(); 

            // Step 1: Register user
            ApiResponseWeb result = await _authWebService.RegisterAsync(_mapper.Map<RegisterModel>(obj));

            if (result != null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                    obj.Role = StaticDetails.SessionRoleUser;

                // Step 2: Assign Role
                var assignRole = await _authWebService.AssignRoleAsync(_mapper.Map<RegisterModel>(obj));
                if (assignRole == null || !assignRole.IsSuccess)
                {
                    _logger.LogError("Assigning role failed for user {Email}.", obj.Email);
                    TempData["error"] = assignRole?.ErrorMessages != null && assignRole.ErrorMessages.Any()
                        ? string.Join(", ", assignRole.ErrorMessages)
                        : "Role assignment failed.";
                    return View(obj);
                }


                if (string.IsNullOrEmpty(obj.Country))
                {
                    obj.Country = StaticDetails.CountryIndia;
                }




                //if (string.IsNullOrEmpty(obj.Country))
                //{
                //    obj.Country = $"{StaticDetails.CountryIndia}, {StaticDetails.CountryUS}, {StaticDetails.CountryUK}"; // Default to all three countries if not provided
                //}


                // Step 3: Check for Country
                //if (string.IsNullOrWhiteSpace(obj.Country))
                //{
                //    _logger.LogWarning("Country is missing for user {Email}.", obj.Email);
                //    TempData["error"] = "Country is required.";
                //    return View(obj);
                //}

                // Step 4: Assign Country
                _logger.LogInformation("Assigning country to user {Email}.", obj.Email);
                var assignCountry = await _authWebService.CountryAssignAsync(_mapper.Map<RegisterModel>(obj));

                if (assignCountry != null && assignCountry.IsSuccess)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    _logger.LogError("Assign country failed for user {Email}.", obj.Email);
                    TempData["error"] = assignCountry?.ErrorMessages != null && assignCountry.ErrorMessages.Any()
                        ? string.Join(", ", assignCountry.ErrorMessages)
                        : "Country assignment failed.";
                    return View(obj);
                }
            }
            else
            {
                _logger.LogError("Registration failed for user {Email}.", obj.Email);
                TempData["error"] = result?.ErrorMessages != null && result.ErrorMessages.Any()
                    ? string.Join(", ", result.ErrorMessages)
                    : "Registration failed.";
                return View(obj); 
            }
        }




        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                var forgetPasswordRequest = _mapper.Map<ForgetPasswordModel>(model);

                var response = await _authWebService.ForgetPasswordAsync(forgetPasswordRequest);

                if (response != null && response.IsSuccess)
                {
                    _logger.LogInformation("Forget password request processed successfully for email {Email}.", model.Email);
                    TempData["SuccessMessage"] = "Password reset instructions have been sent to your email.";
                    return RedirectToAction("Login", "Auth");
                }

                _logger.LogWarning("Forget password request failed for email {Email}.", model.Email);
                ModelState.AddModelError("", "Failed to process forget password request. Please try again.");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing forget password request for email {Email}.", model.Email);
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                var changePasswordRequest = _mapper.Map<ChangePasswordModel>(model);

                var response = await _authWebService.ChangePasswordAsync(changePasswordRequest);

                if (response != null && response.IsSuccess)
                {
                    _logger.LogInformation("Password changed successfully for user {Email}.", model.Email);
                    TempData["SuccessMessage"] = "Your password has been changed successfully.";
                    return RedirectToAction("Login", "Auth");
                }

                _logger.LogWarning("Password change failed for user {Email}.", model.Email);
                ModelState.AddModelError("", "Failed to change password. Please try again.");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while changing password for user {Email}.", model.Email);
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Logic to clear session or token
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(StaticDetails.TokenCookie, string.Empty);
            _logger.LogInformation("User logged out successfully.");
            return RedirectToAction("Login", "Auth");
        }


        [HttpGet]
        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            _logger.LogWarning("Login attempt failed due to invalid model state for user {UserEmail}.", model.Email);
        //            return View(model);
        //        }

        //        _logger.LogInformation("Processing login request for user {UserEmail}.", model.Email);

        //        // Call the API and get the response
        //        ApiResponseWeb responseDto = await _authWebService.LoginAsync(_mapper.Map<LoginModel>(model));

        //        // Log the API response
        //        _logger.LogInformation("API Response: {@ResponseDto}", responseDto);

        //        if (responseDto != null && responseDto.IsSuccess)
        //        {
        //            _logger.LogInformation("Login successful for user {Email}.", model.Email);

        //            // Deserialize the response and validate
        //            var loginResponseDto = JsonConvert.DeserializeObject<LoginResponseWebDto>(Convert.ToString(responseDto.Result));
        //            if (loginResponseDto == null || string.IsNullOrEmpty(loginResponseDto.Token))
        //            {
        //                _logger.LogError("Deserialization failed or token is null for user {Email}.", model.Email);
        //                TempData["error"] = "An error occurred while processing your login. Please try again.";
        //                return View(model);
        //            }

        //            await SignInUser2(loginResponseDto);
        //            _tokenProvider.SetToken(loginResponseDto.Token);

        //            _logger.LogInformation("User {Email} signed in and token set successfully.", model.Email);


        //            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        //            if (string.IsNullOrEmpty(role))
        //            {
        //                _logger.LogError("Role claim is missing for user {Email}.", model.Email);
        //                TempData["error"] = "Role claim is missing.";
        //                return View(model);
        //            }

        //            if (role == StaticDetails.SessionRoleAdmin)
        //            {
        //                 _logger.LogError("User {Email} has Admin role.", model.Email);
        //                return RedirectToAction("Index", "AdminDashboard", new { area = "Admin" });
        //            }
        //            else if (role == StaticDetails.SessionRoleStaff)
        //            {
        //                _logger.LogError("User {Email} has Staff role.", model.Email);
        //                return RedirectToAction("Index", "StaffDashboard", new { area = "Staff" });
        //            }
        //            else if (role == StaticDetails.SessionRoleUser)
        //            {
        //                _logger.LogError("User {Email} has User role.", model.Email);
        //                return RedirectToAction("Index", "UserDashboard", new { area = "User" });
        //            }

        //            _logger.LogWarning("No matching role found for user {Email}.", model.Email);
        //            return RedirectToAction("Login");

        //        }
        //        else
        //        {
        //            _logger.LogWarning("Login failed for user {Email}. Error: {ErrorMessages}", model.Email, responseDto?.ErrorMessages);
        //            TempData["error"] = responseDto?.ErrorMessages;
        //            return View(model);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
              
        //        _logger.LogError(ex, "An error occurred while processing the login for user {UserEmail}. Stack Trace: {StackTrace}", model.Email, ex.StackTrace);

        //        ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
               
        //        return RedirectToAction("Login", "Auth");
        //    }
        //}

        private async Task SignInUser2(LoginResponseWebDto model)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(model.Token);

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                // Extract and validate claims
                var nameIdentifier = jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(nameIdentifier)) throw new Exception("NameIdentifier claim is missing.");
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));

                var name = jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(name)) throw new Exception("Name claim is missing.");
                identity.AddClaim(new Claim(ClaimTypes.Name, name));

                var email = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value;
                if (string.IsNullOrEmpty(email)) throw new Exception("Email claim is missing.");
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, email));
                identity.AddClaim(new Claim(ClaimTypes.Email, email));

                var jti = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Jti)?.Value;
                if (string.IsNullOrEmpty(jti)) throw new Exception("JTI claim is missing.");
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, jti));

                var role = jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                //var country = jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Country)?.Value;
                //if (!string.IsNullOrEmpty(country))
                //{
                //    identity.AddClaim(new Claim(ClaimTypes.Country, country));
                //}

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while signing in the user. Stack Trace: {StackTrace}", ex.StackTrace);
                throw; // Re-throw the exception to ensure it propagates if necessary
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Login attempt failed due to invalid model state for user {UserEmail}.", model.Email);
                    return View(model);
                }

                _logger.LogInformation("Processing login request for user {UserEmail}.", model.Email);

                // Call the API and get the response
                ApiResponseWeb responseDto = await _authWebService.LoginAsync(_mapper.Map<LoginModel>(model));

                if (responseDto == null || !responseDto.IsSuccess)
                {
                    _logger.LogWarning("Login failed for user {Email}. Error: {ErrorMessages}", model.Email, responseDto?.ErrorMessages);
                    TempData["error"] = responseDto?.ErrorMessages != null && responseDto.ErrorMessages.Any()
                        ? string.Join(", ", responseDto.ErrorMessages)
                        : "Login failed. Please try again.";
                    return View(model);
                }

                // Deserialize the response and validate
                var loginResponseDto = JsonConvert.DeserializeObject<LoginResponseWebDto>(Convert.ToString(responseDto.Result));
                if (loginResponseDto == null || string.IsNullOrEmpty(loginResponseDto.Token))
                {
                    _logger.LogError("Deserialization failed or token is null for user {Email}.", model.Email);
                    TempData["error"] = "An error occurred while processing your login. Please try again.";
                    return View(model);
                }

                await SignInUser2(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);

                _logger.LogInformation("User {Email} signed in and token set successfully.", model.Email);

                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (string.IsNullOrEmpty(role))
                {
                    _logger.LogError("Role claim is missing for user {Email}.", model.Email);
                    TempData["error"] = "Role claim is missing.";
                    return View(model);
                }

                return role switch
                {
                    nameof(StaticDetails.SessionRoleAdmin) => RedirectToAction("Index", "AdminDashboard", new { area = "Admin" }),
                    nameof(StaticDetails.SessionRoleStaff) => RedirectToAction("Index", "StaffDashboard", new { area = "Staff" }),
                    nameof(StaticDetails.SessionRoleUser) => RedirectToAction("Index", "UserDashboard", new { area = "User" }),
                    _ => RedirectToAction("Login")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the login for user {UserEmail}. Stack Trace: {StackTrace}", model.Email, ex.StackTrace);
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                return View(model);
            }
        }




    }
}

using AuthExcelService.Domain.Dtos.Auth;
using AuthExcelService.Domain.Entities;
using AuthExcelService.Persistence.Data;
using AuthExcelService.Repositoies.Contracts;
using AuthExcelService.Repositoies.Contracts.ITokenService;
using AuthExcelService.Repositoies.IGenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Repositoies.RepositoryService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<AuthService> _logger;

        private readonly ApplicationDBContext1 _dbContext;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IJwtTokenGenerator jwtTokenGenerator, ILogger<AuthService> logger, RoleManager<ApplicationRole> roleManager, ApplicationDBContext1 dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            _logger.LogInformation("Attempting to change password for user: {email}", email);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("User not found: {UserName}", email);
                return false;
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                _logger.LogInformation("Password changed successfully for user: {UserName}", email);
            }
            else
            {
                _logger.LogError("Failed to change password for user: {UserName}. Errors: {Errors}",
                    email, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return result.Succeeded;
        }

        public async Task<bool> ForgetPasswordAsync(string email)
        {
            _logger.LogInformation("Processing forget password request for email: {Email}", email);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("User not found with email: {Email}", email);
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            _logger.LogInformation("Generated password reset token for email: {Email}", email);

            // Log token for testing purposes (remove in production)
            Console.WriteLine($"Password reset token for {email}: {token}");

            return true;
        }

        public async Task<bool> IsUniqueUser(string username)
        {
            _logger.LogInformation("Checking if username is unique: {UserName}", username);

            var user = await _userManager.FindByNameAsync(username);
            var isUnique = user == null;

            _logger.LogInformation("Is username unique: {IsUnique}", isUnique);
            return isUnique;
        }

        public async Task<LoginResponseDto> Login(LoginDto loginRequestDTO)
        {
            _logger.LogInformation("Attempting login for user: {UserName}", loginRequestDTO?.Email);

            if (string.IsNullOrWhiteSpace(loginRequestDTO?.Email))
            {
                _logger.LogError("Login failed: Username is null or empty.");
                throw new ArgumentException("Username cannot be null or empty.", nameof(loginRequestDTO.Email));
            }

            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Email);
            if (user == null)
            {
                _logger.LogWarning("Login failed: User not found with username: {UserName}", loginRequestDTO.Email);
                return null!;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDTO.Password, false);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Login failed: Invalid credentials for user: {UserName}", loginRequestDTO.Email);
                return null!;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var roleName = roles.FirstOrDefault() ?? string.Empty;

            var UserCountries = await _dbContext.UserCountries
                .Include(uc => uc.Country)
                .Where(uc => uc.UserId == user.Id)
                .ToListAsync();

          
            var countryName = UserCountries.FirstOrDefault()?.Country.CountryName ?? string.Empty;



            //var role = new RoleDto
            //{
            //    Id = Guid.NewGuid(),
            //    Name = roleName
            //};

            _logger.LogInformation("Roles for user {UserId}: {Roles}", user.Id, string.Join(", ", roles));

            var token = await _jwtTokenGenerator.GenerateToken(user, roles);

            _logger.LogInformation("Login successful for user: {UserName}", loginRequestDTO.Email);

            return new LoginResponseDto
            {
                Token = token,
                User = new UserDto
                {
                    ID = user.Id.ToString(),
                    UserName = user.UserName,
                    Name = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    Roles = new RoleDto
                    {
                        Id = Guid.NewGuid(), // Replace with actual role ID if available
                        Name = roleName
                    },
                    Countries = new CountryDto
                    { 
                      
                        Id = Guid.NewGuid(),
                        CountryName = countryName
                    } // Replace with actual country ID if available


                }

            };
        }

        public async Task<bool> AssignRolesAsync(string email, List<string> roleNames)
        {
            _logger.LogInformation("Starting role assignment for user with email: {Email}", email);

            var user = await _userManager.FindByEmailAsync(email.ToLower());
            if (user == null)
            {
                _logger.LogWarning("User not found with email: {Email}", email);
                return false;
            }

            if (roleNames == null || !roleNames.Any())
            {
                _logger.LogWarning("No roles provided for assignment to user with email: {Email}", email);
                return false;
            }

            foreach (var roleName in roleNames)
            {
                _logger.LogInformation("Processing role: {RoleName} for user: {Email}", roleName, email);

                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    _logger.LogInformation("Role '{RoleName}' does not exist. Creating role.", roleName);
                    var roleResult = await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                    if (!roleResult.Succeeded)
                    {
                        _logger.LogError("Failed to create role '{RoleName}'. Errors: {Errors}",
                            roleName, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                        return false;
                    }
                }

                if (!await _userManager.IsInRoleAsync(user, roleName))
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName);
                    if (!addToRoleResult.Succeeded)
                    {
                        _logger.LogError("Failed to assign role '{RoleName}' to user with email: {Email}. Errors: {Errors}",
                            roleName, email, string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));
                        return false;
                    }
                    _logger.LogInformation("Successfully assigned role '{RoleName}' to user with email: {Email}", roleName, email);
                }
                else
                {
                    _logger.LogInformation("User with email: {Email} is already in role: {RoleName}", email, roleName);
                    // Return true if the user is already in the role
                    return true;
                }

            }

            _logger.LogInformation("Completed role assignment for user with email: {Email}", email);
            return true;
        }

        public async Task<UserDto> Register(RegisterationDto registrationRequestDto)
        {
            if (registrationRequestDto == null)
            {
                throw new ArgumentNullException(nameof(registrationRequestDto), "Registration request cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(registrationRequestDto.Password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(registrationRequestDto.Password));
            }

            if (string.IsNullOrWhiteSpace(registrationRequestDto.Email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(registrationRequestDto.Email));
            }

            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.UserName,
                Email = registrationRequestDto.Email,
                FirstName = registrationRequestDto.FirstName,
                LastName = registrationRequestDto.LastName,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                EmailConfirmed = true

            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = await _userManager.FindByEmailAsync(registrationRequestDto.Email);
                    if (userToReturn == null)
                    {
                        throw new InvalidOperationException("User creation succeeded but user could not be retrieved.");
                    }

                    // Assign role if provided
                    if (!string.IsNullOrWhiteSpace(registrationRequestDto.Role))
                    {
                        var roleAssigned = await AssignRolesAsync(userToReturn.Email, new List<string> { registrationRequestDto.Role });
                        if (!roleAssigned)
                        {
                            _logger.LogWarning("Failed to assign role '{Role}' to user '{Email}' during registration.", registrationRequestDto.Role, registrationRequestDto.Email);
                        }
                    }


                    // Assign country if provided
                    if (!string.IsNullOrWhiteSpace(registrationRequestDto.Country))
                    {
                        var countryAssigned = await AssignCountrytoUserAsync(userToReturn.Email, new List<string> { registrationRequestDto.Country });
                        if (!countryAssigned)
                        {
                            _logger.LogWarning("Failed to assign country '{Country}' to user '{Email}' during registration.", registrationRequestDto.Country, registrationRequestDto.Email);
                        }
                    }


                    return new UserDto
                    {
                        Email = userToReturn.Email ?? string.Empty,
                        ID = userToReturn.Id.ToString(),
                        Name = $"{userToReturn.FirstName} {userToReturn.LastName}",
                        UserName = userToReturn.UserName ?? string.Empty,
                        Countries = new CountryDto
                        {
                            Id = Guid.NewGuid(), 
                            CountryName = registrationRequestDto.Country??string.Empty 
                        },
                        Roles = new RoleDto
                        {
                            Id = Guid.NewGuid(), 
                            Name = registrationRequestDto.Role ?? string.Empty 
                        }
                        
                        


                    };
                }
                else
                {
                    var errorDescription = result.Errors.FirstOrDefault()?.Description ?? "Unknown error occurred.";
                    throw new InvalidOperationException(errorDescription);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during user registration.");
                throw; // Re-throw the exception to ensure proper error handling.
            }
        }


        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            return new UserDto
            {
                ID = user.Id.ToString(),
                UserName = user.UserName,
                Email = user.Email
            };
        }



        /// <summary>
        /// Saves a password reset token for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="resetToken">The reset token to save.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SavePasswordResetTokenAsync(string userId, string resetToken)
        {
            var passwordResetToken = new PasswordResetToken
            {
                UserId = Guid.Parse(userId),
                Token = resetToken,
                ExpirationDate = DateTime.UtcNow.AddHours(24) // Token valid for 24 hours
            };

            await _dbContext.PasswordResetTokens.AddAsync(passwordResetToken);
            await _dbContext.SaveChangesAsync();
        }

        


        public async Task<bool> AssignCountrytoUserAsync1(string email, List<string> countryNames)
        {
            _logger.LogInformation("Starting country assignment for user with email: {Email}", email);

            var user = await _userManager.FindByEmailAsync(email.ToLower());
            if (user == null)
            {
                _logger.LogWarning("User not found with email: {Email}", email);
                return false;
            }

            if (countryNames == null || !countryNames.Any())
            {
                _logger.LogWarning("No countries provided for assignment to user with email: {Email}", email);
                return false;
            }

            if (user.UserCountries == null)
                user.UserCountries = new List<UserCountry>();

            foreach (var countryName in countryNames)
            {
                var countryEntity = await _dbContext.Countries .FirstOrDefaultAsync(c => c.CountryName.ToLower() == countryName.ToLower());


                if (countryEntity == null)
                {
                    _logger.LogWarning("Country '{CountryName}' not found in database. Skipping.", countryName);
                    continue;
                }

                bool alreadyAssigned = user.UserCountries
                    .Any(uc => uc.CountryId == countryEntity.Id);

                if (!alreadyAssigned)
                {
                    user.UserCountries.Add(new UserCountry
                    {
                        CountryId = countryEntity.Id,
                        UserId = user.Id
                    });

                    _logger.LogInformation("Country '{Country}' assigned to user {Email}", countryName, email);
                }
                else
                {
                    _logger.LogInformation("User {Email} already has access to country: {Country}", email, countryName);
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                _logger.LogError("Failed to update user {Email} during country assignment. Errors: {Errors}",
                    email, string.Join(", ", result.Errors.Select(e => e.Description)));
                return false;
            }

            _logger.LogInformation("Successfully completed country assignment for user {Email}", email);
            return true;
        }


        public async Task<bool> AssignCountrytoUserAsync(string email, List<string> countryNames)
        {
            _logger.LogInformation("Starting country assignment for user with email: {Email}", email);

            // ✅ Step 1: Load user with UserCountries
            var user = await _userManager.Users
                .Include(u => u.UserCountries)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (user == null)
            {
                _logger.LogWarning("User not found with email: {Email}", email);
                return false;
            }

            if (countryNames == null || !countryNames.Any())
            {
                _logger.LogWarning("No countries provided for assignment to user with email: {Email}", email);
                return false;
            }

            if (user.UserCountries == null)
                user.UserCountries = new List<UserCountry>();

            foreach (var countryName in countryNames)
            {
                // ✅ Step 2: Get the Country entity
                var countryEntity = await _dbContext.Countries
                    .FirstOrDefaultAsync(c => c.CountryName.ToLower() == countryName.ToLower());

                if (countryEntity == null)
                {
                    _logger.LogWarning("Country '{CountryName}' not found in database. Skipping.", countryName);
                    continue;
                }

                // ✅ Step 3: Check if already assigned
                bool alreadyAssigned = user.UserCountries
                    .Any(uc => uc.CountryId == countryEntity.Id);

                if (!alreadyAssigned)
                {
                    user.UserCountries.Add(new UserCountry
                    {
                        CountryId = countryEntity.Id,
                        UserId = user.Id
                    });

                    _logger.LogInformation("Country '{Country}' assigned to user {Email}", countryName, email);
                }
                else
                {
                    _logger.LogInformation("User {Email} already has access to country: {Country}", email, countryName);
                }
            }

            // ✅ Step 4: Update user
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                _logger.LogError("Failed to update user {Email} during country assignment. Errors: {Errors}",
                    email, string.Join(", ", result.Errors.Select(e => e.Description)));
                return false;
            }

            _logger.LogInformation("Successfully completed country assignment for user {Email}", email);
            return true;
        }






    }
}

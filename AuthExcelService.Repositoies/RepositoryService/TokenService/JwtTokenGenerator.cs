using AuthExcelService.Domain.Entities;
using AuthExcelService.Repositoies.Contracts.ITokenService;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace AuthExcelService.Repositoies.RepositoryService.TokenService
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<JwtTokenGenerator> _logger;

        public JwtTokenGenerator(JwtSettings jwtSettings, ILogger<JwtTokenGenerator> logger)
        {
            _jwtSettings = jwtSettings;
            _logger = logger;
        }

        //public async Task<string> GenerateToken(ApplicationUser user, IList<string> roles)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Generating JWT token for user: {UserId}", user.Id);

        //        var authClaims = new List<Claim>
        //       {
        //           new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //           new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
        //           new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
        //           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //       };

        //        foreach (var role in roles)
        //        {
        //            authClaims.Add(new Claim(ClaimTypes.Role, role));
        //        }

        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        //        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //        var token = new JwtSecurityToken(
        //            issuer: _jwtSettings.Issuer,
        //            audience: _jwtSettings.Audience,
        //            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
        //            claims: authClaims,
        //            signingCredentials: credentials
        //        );

        //        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        //        _logger.LogInformation("Generated Token: {token}", tokenString);
        //        _logger.LogInformation("JWT token generated successfully for user: {UserId}", user.Id);

        //        return await Task.FromResult(tokenString);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while generating JWT token for user: {UserId}", user.Id);
        //        throw;
        //    }
        //}

        //////////////////////////////////////
        //public async Task<string>  GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

        //    var claimList = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
        //        new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id),
        //        new Claim(JwtRegisteredClaimNames.Name,applicationUser.UserName)
        //    };

        //    claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Audience = _jwtSettings.Audience,
        //        Issuer = _jwtSettings.Issuer,
        //        Subject = new ClaimsIdentity(claimList),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}


        //------------------------


        public async Task<string> GenerateToken2(ApplicationUser user, IList<string> roles)
        {
            try
            {
                _logger.LogInformation("Generating JWT token for user: {UserId}", user.Id);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                    //new Claim(ClaimTypes.Country, user.UserCountries != null ? string.Join(",", user.UserCountries.Select(uc => uc.Country.CountryName)) : string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                // Add claims for each country associated with the user
                //if (user.UserCountries != null)
                //{
                //    foreach (var userCountry in user.UserCountries)
                //    {
                //        authClaims.Add(new Claim(ClaimTypes.Country, userCountry.Country.CountryName ?? string.Empty));
                //    }
                //}

                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                _logger.LogInformation("User Roles: {Roles}", string.Join(", ", roles));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                    claims: authClaims,
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                _logger.LogInformation("Generated Token: {token}", tokenString);
                _logger.LogInformation("JWT token generated successfully for user: {UserId}", user.Id);

                return await Task.FromResult(tokenString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating JWT token for user: {UserId}. StackTrace: {StackTrace}", user.Id, ex.StackTrace);
                throw;
            }

        }


        public async Task<string> GenerateToken(ApplicationUser user, IList<string> roles)
        {
            try
            {
                _logger.LogInformation("Generating JWT token for user: {UserId}", user.Id);

                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                        new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                // Add Role claims
                if (roles != null && roles.Any())
                {
                    foreach (var role in roles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    _logger.LogInformation("User Roles: {Roles}", string.Join(", ", roles));
                }
                else
                {
                    _logger.LogWarning("No roles provided for user: {UserId}", user.Id);
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                    claims: authClaims,
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                _logger.LogInformation("Generated Token: {token}", tokenString);
                _logger.LogInformation("JWT token generated successfully for user: {UserId}", user.Id);

                return await Task.FromResult(tokenString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating JWT token for user: {UserId}. StackTrace: {StackTrace}", user.Id, ex.StackTrace);
                throw;
            }
        }

    }
}

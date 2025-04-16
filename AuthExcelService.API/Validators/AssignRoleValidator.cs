using AuthExcelService.Domain.Dtos.Auth;
using AuthExcelService.Repositoies.Contracts;
using FluentValidation;

namespace AuthExcelService.API.Validators
{
    public class AssignRoleValidator : AbstractValidator<AssginRoleDto>
    {
        private readonly IAuthService _roleService;

        public AssignRoleValidator(IAuthService roleService)
        {
            _roleService = roleService;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Role)
                 .NotEmpty().WithMessage("Role is required.");
                 //.Must((dto, role) =>
                 //{
                 //    try
                 //    {
                 //        var isAssigned = _roleService.AssignRolesAsync(
                 //            dto.Email ?? string.Empty,
                 //            new List<string> { role ?? string.Empty }
                 //        ).GetAwaiter().GetResult();
                 //        return !isAssigned;
                 //    }
                 //    catch (Exception ex)
                 //    {
                 //        // Log the exception if needed
                 //        throw new ValidationException($"Error encountered: {ex.Message}");
                 //    }
                 //})
                 //.WithMessage(dto => $"User already in role '{dto.Role}'.");
        }
    }
}

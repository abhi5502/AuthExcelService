using AuthExcelService.API.Models.Dtos;
using FluentValidation;

namespace AuthExcelService.API.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.CurrentPassword).NotEmpty();
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(6).WithMessage("New password must be at least 6 characters long");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Confirm password must match new password");
        }
    }
}

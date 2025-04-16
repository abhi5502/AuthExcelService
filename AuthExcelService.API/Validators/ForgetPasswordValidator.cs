using AuthExcelService.API.Models.Dtos;
using FluentValidation;

namespace AuthExcelService.API.Validators
{
    public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordDto>
    {
        public ForgetPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");
        }
    }
}

using AuthExcelService.Domain.Dtos.Auth;
using FluentValidation;

namespace AuthExcelService.API.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterationDto>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Valid email is required");
        }
    }
}

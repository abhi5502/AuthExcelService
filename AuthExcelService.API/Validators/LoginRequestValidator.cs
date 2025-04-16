using AuthExcelService.Domain.Dtos.Auth;
using FluentValidation;

namespace AuthExcelService.API.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be a valid email address")
                .MinimumLength(3).WithMessage("Email must be at least 3 characters long");


            RuleFor(x => x.Password)
                           .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d).+$").WithMessage("Password must contain at least one letter and one number")
                .Must(password => !password.Contains("password", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Password cannot contain the word 'password'");
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace AuthExcelService.WebApp.Models.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression(@"^[a-zA-Z0-9_!@#$%^&*.]{3,20}$", ErrorMessage = "Username must be 3-20 characters long and can only contain letters, numbers, underscores, dots, and special characters (!@#$%^&*).")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must have at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression(@"^(Admin|Staff|User)$", ErrorMessage = "Role must be either Admin, User, or Manager.")]
        public string Role { get; set; } = null!;

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } = null!;

        [Required(ErrorMessage = "First Name is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string? LastName { get; set; }
    }
}

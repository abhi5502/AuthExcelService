using Microsoft.AspNetCore.Identity;

namespace AuthExcelService.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserCountry> UserCountries { get; set; } = new List<UserCountry>();
    }
}
using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public String? FirstName { get; set; }
        public String? LastName { get; set; }

        // Refresh Token
        public String? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        // Ref: navigation property
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
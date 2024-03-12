using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public String? FirstName { get; set; }
        public String? LastName { get; set; }

        // Ref: navigation property
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
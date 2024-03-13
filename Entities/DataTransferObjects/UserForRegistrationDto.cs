using System.ComponentModel.DataAnnotations;
using Entities.Models;

namespace Entities.DataTransferObjects
{
    public record UserForRegistrationDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public int DepartmentId { get; init; }

        [Required(ErrorMessage = "Username is required.")] // Username de zorunluluk
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; init; }

        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }

        public ICollection<string>? Roles { get; init; } // init-> tanımlandığı yerde değerlerin verilmesi gerekiyor.

    }
}
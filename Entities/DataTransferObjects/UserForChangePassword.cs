using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record UserForChangePassword
    {
        [Required(ErrorMessage = "Current Password is required.")]
        public string? CurrentPassword { get; init; }

        [Required(ErrorMessage = "New Password is required.")]
        public string? NewPassword { get; init; }

        [Required(ErrorMessage = "New Password Again is required.")]
        public string? NewPasswordAgain { get; init; }
    }
}

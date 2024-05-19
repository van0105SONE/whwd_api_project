using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ApplicationCore.Dtos.AuthenticationDto
{
    public class PasswordResetDto
    {
        [Required]
        public required String UserName { get; set; }
        [Required]
        public required String CurrentPassword { get; set; }

        [Required]
        public required String NewPasssword { get; set; }

    }
}

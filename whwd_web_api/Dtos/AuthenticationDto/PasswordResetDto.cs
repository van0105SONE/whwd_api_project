using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace whwd_web_api.Dtos.AuthenticationDto
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

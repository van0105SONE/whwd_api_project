using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ApplicationCore.Dtos.AuthenticationDto
{
    public class PasswordResetDto
    {
        public String? userName { get; set; }

        public  String? currentPassword { get; set; }
        public  String? newPassword { get; set; }

    }
}

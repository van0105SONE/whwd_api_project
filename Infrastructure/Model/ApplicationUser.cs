using Infrastructure.Model.Volunteer;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Model
{
    public class ApplicationUser : IdentityUser
    {
        public String Fname { get; set; }
        public String Lname { get; set; }
        public String Occupation { get; set; }
        public Major Major { get; set; }
        public Generation Generation { get; set; }
        public String? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
    }
}

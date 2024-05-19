using Infrastructure.Model.Address;
using Infrastructure.Model.University;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Model.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Occupation { get; set; }

        public Village CurrentVillage { get; set; }
        public Village BornVillage { get; set; }
        public UserType UserType { get; set; }
        public Major Major { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        public ICollection<PositionTeam> positionTeams { get; set; }
    }
}

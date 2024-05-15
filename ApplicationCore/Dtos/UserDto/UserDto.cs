using ApplicationCore.Dtos.Roles;
using Infrastructure.Model.Address;
using Infrastructure.Model.University;
using Infrastructure.Model.Users;
using whwd_web_api.Dtos.Address;

namespace ApplicationCore.Dtos.UserDto
{
    public class UserDto
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Occupation { get; set; }

        public VillageDto CurrentVillage { get; set; }
        public VillageDto BornVillage { get; set; }
        public UserType UserType { get; set; }
        public Major Major { get; set; }
        public PositionTeamDto positionTeam { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

    }

    public class UserUpdateDto : UserDto
    {
       public string Id { get; set; }
    }
}

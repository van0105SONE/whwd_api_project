using ApplicationCore.Dtos.Roles;
using ApplicationCore.Dtos.University;
using whwd_web_api.Dtos.Address;


namespace ApplicationCore.Dtos.UserDto
{
    public class UserDto
    {
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Occupation { get; set; }

        public required string typeId { get; set; }
        public required Guid positionId {get; set;}
        public required Guid teamId {get; set;}
        public required VillageDto CurrentVillage { get; set; }
        public required VillageDto BornVillage { get; set; }
        public required MajorDto Major { get; set; }
    }

 public class UserUpdateDto{
     public required string Id {get; set;}
 }

}

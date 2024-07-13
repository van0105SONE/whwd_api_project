using ApplicationCore.Dtos.Roles;
using ApplicationCore.Dtos.University;
using whwd_web_api.Dtos.Address;

namespace ApplicationCore.Dtos.UserDto
{
	public class UserReponseDto
	{
		public string Id { get; set; }
		public string Fname { get; set; }
		public string Lname { get; set; }
		public string Occupation { get; set; }

		public VillageDto CurrentVillage { get; set; }
		public VillageDto BornVillage { get; set; }
		public MajorDto Major { get; set; }
		public RoleDto Role { get; set; }


    }
}

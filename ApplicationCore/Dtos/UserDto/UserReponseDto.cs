using ApplicationCore.Dtos.Address;
using ApplicationCore.Dtos.Roles;
using ApplicationCore.Dtos.University;
using whwd_web_api.Dtos.Address;

namespace ApplicationCore.Dtos.UserDto
{
	public class UserReponseDto
	{
		public string id { get; set; }
		public string fname { get; set; }
		public string lname { get; set; }
		public string occupation { get; set; }

		public VillageReponseDto currentVillage { get; set; }
		public VillageReponseDto bornVillage { get; set; }
		public MajorDto major { get; set; }
		public RoleDto role { get; set; }


    }
}

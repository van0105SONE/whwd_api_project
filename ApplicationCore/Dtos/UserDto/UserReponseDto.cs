using ApplicationCore.Dtos.Address;
using ApplicationCore.Dtos.RoleDto;
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
		public string userName { get; set; }
		public string occupation { get; set; }
		public string gender { get; set; }
		public string email { get; set; }

		public string phoneNumber { get; set; }



		public DateTime birtDate { get; set; }
		public VillageReponseDto currentVillage { get; set; }
		public VillageReponseDto bornVillage { get; set; }
		public MajorResponseDto major { get; set; }
		public RoleResponse role { get; set; }

		public PositionResponse position { get; set; }
		public ProjectTeamResponse projectTeam { get; set; }


    }
}

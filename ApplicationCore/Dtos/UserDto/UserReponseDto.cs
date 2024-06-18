using ApplicationCore.Dtos.Roles;
using ApplicationCore.Dtos.University;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		public UserTypeDto UserType { get; set; }
		public MajorDto Major { get; set; }

	}
}

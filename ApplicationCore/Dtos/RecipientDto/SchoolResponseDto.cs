using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using whwd_web_api.Dtos.Address;

namespace ApplicationCore.Dtos.RecipientDto
{
	public class SchoolResponseDto
	{
		public Guid Id { get; set; }
		public required string Name { get; set; }
		public VillageDto Village { get; set; }
	}
}

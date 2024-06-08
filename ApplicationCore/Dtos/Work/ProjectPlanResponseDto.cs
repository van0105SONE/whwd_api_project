using ApplicationCore.Dtos.RecipientDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Work
{
	public class ProjectPlanResponseDto
	{
		public Guid Id { get; set; }
		public String ProjectName { get; set; }
		public String Description { get; set; }
		public String userId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public List<SchoolResponseDto> schools { get; set; }
		public ICollection<DonateThingResponseDto> donateThings { get; set; }
	}
}

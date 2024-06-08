using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Work
{
	public class DonateThingResponseDto
	{
		public Guid Id { get; set; }
		public String Name { get; set; }
		public Double Price { get; set; }

		public string UnitType { get; set; }
		public int Unit { get; set; }
		public int personAmount { get; set; }
	}
}

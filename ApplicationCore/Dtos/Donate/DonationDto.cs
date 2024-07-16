using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Donate
{
	public class DonationDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public double amount { get; set; }
		public string? Name { get; set; }
		public string? SponsorType { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Facebook { get; set; }
	    public string? DonationType { get; set; }
		public string userId { get; set; }
		public Guid  DonatorId { get; set; }

		public Guid accountId { get; set; }
	}

}

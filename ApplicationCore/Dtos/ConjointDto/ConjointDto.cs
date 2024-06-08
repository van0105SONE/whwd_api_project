using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.ConjointDto
{
	public class ConjointDto
	{
		public string JoinerId { get; set; }
		public bool JointFundRaising { get; set; }
		public bool JointCashCalculate { get; set; }
		public string UserId { get; set; }
		public Guid FundRaisingPlaceId { get; set; }
	}

	public class ConjointUpdateDto : ConjointDto
	{
		public Guid Id { get; set; }
	}
}

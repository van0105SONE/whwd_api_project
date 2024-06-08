using Infrastructure.Model.Place;
using Infrastructure.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Work
{
	public class Conjoint : BaseModel
	{
		public ApplicationUser Joiner { get; set; }
		public bool JointFundRaising { get; set; }
		public bool JointCashCalculate { get; set; }
		public FundRaisingPlace FundRaisingPlace { get; set; }
	}
}

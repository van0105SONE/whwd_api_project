using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Roles
{
	public class PositionResponse
	{
		public Guid Id { get; set; }
		public string RefNo { get; set; }
		public string PositionName { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.TransactionDto
{
	public class TransactionDto
	{
		/// <summary>
		/// Description.
		/// </summary>
		/// <example>b521fb69-d6fc-4c20-83bf-46a3f391eb52</example>
		public required string Description { get; set; }
		public required Guid  TransactionTypeId { get; set; }
		public required Guid SourceTypeId { get; set; }
		public double Amount { get; set; }

		public required Guid  AccountId;
		public required string UserId { get; set; }
	}
}

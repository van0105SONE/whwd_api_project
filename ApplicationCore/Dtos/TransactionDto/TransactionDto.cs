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
		public required string description { get; set; }
		public required string transactionType { get; set; }
		public double amount { get; set; }
		public required Guid accountId { get; set; }
		public required string userId { get; set; }
	}
}

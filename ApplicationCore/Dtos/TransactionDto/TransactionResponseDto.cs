using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.TransactionDto
{
    public class TransactionResponseDto
    {
        public required string description { get; set; }
        public required string transactionType { get; set; }
        public double amount { get; set; }
    }
}

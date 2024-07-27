using ApplicationCore.Dtos.Accounts;
using ApplicationCore.Dtos.Reports;
using ApplicationCore.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.TransactionDto
{
    public class TransactionResponseDto
    {
        public DateTime createAt { get; set; }
        public required string description { get; set; }
        public required string transactionType { get; set; }
        public double amount { get; set; }

        public AccountResponseDto account { get; set; }

        public UserReponseDto createBy { get; set; }
    }
}

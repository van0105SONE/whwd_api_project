using ApplicationCore.Dtos.UserDto;
using ApplicationCore.Dtos.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Accounts
{
    public class AccountResponseDto
    {
        public required string AccountNo { get; set; }
        public required string BookingNO { get; set; }
        public required string AccountTypes { get; set; }
        public required UserReponseDto OwnBy { get; set; }
        public required ProjectPlanResponseDto ProjectPlan { get; set; }
        public double DepositAmount { get; set; }
        public double WithdrawAmount { get; set; }
        public double Balance { get; set; }
    }
}

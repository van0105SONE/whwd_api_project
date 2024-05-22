using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Account
{
    public class Account : BaseModel
    {
        public required string AccountNo { get; set; }
        public required string BookingNO { get; set; }
        public required string BankName { get; set; }

        public required AccountType AccountTypes { get; set; }
        public required  ApplicationUser OwnBy { get; set; }

        public required ProjectPlan ProjectPlan { get; set; }
        public  double   Balance { get; set; }
    }
}

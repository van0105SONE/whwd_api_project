using Infrastructure.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Account
{
    public class Account : BaseModel
    {
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public string BookingNO { get; set; }
        public string BankName { get; set; }

        public string AccountType { get; set; }
        public  ApplicationUser OwnBy { get; set; }
        public int   Balance { get; set; }
    }
}

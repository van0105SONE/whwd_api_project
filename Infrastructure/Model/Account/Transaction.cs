using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Account
{
    public class Transaction : BaseModel
    {
      public required string Description {get; set;}
      public required TransactionType TransactionType {get; set;}
      public double Amount { get; set; }
      public required  Account Account;
    }
}

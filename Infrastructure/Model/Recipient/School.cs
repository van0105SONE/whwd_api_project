using Infrastructure.Model.Address;
using Infrastructure.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Recipient
{
    public class School : BaseModel
    {
        public required string name { get; set; }
        public int totalFund { get; set; }
        public Village village { get; set; }
        public required ProjectPlan project { get; set; }
    }
}

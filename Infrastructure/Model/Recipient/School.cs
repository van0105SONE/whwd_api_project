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
        public required string Name { get; set; }
        public Village Village { get; set; }
        public required ProjectPlan Project { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Work
{
    public class ProjectPlan : BaseModel
    {
        public String ProjectName { get; set; }
        public String Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DonateThing DonateThing { get; set; }
        public Double  TargetValue { get; set; }       
    }
}

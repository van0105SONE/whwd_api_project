using Infrastructure.Model.Recipient;
using Infrastructure.Model.Users;
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
        public bool IsActive { get; set; }

        public Double  ValueInDollar { get; set; } 
        public Double  ValueInBath { get; set; }
        public Double  valueInKip { get; set; }
        public ICollection<DonateThing> donateThings { get; set; }
		public ICollection<School> schools { get; set; }
	}
}

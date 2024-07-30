using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Filter
{
    public class ContJointPlace : BaseFilter
    {
        public Guid userId { get; set; }
        public Guid placeId { get; set; }
    }
}

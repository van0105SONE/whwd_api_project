using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Filter.report
{
    public class RecipientReportFilter : BaseFilter
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public Guid projectId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.RecipientDto
{
    public class SchoolDto
    {

        public required List<string> Names { get; set; }
        public required string VillageName { get; set; }
        public string villageCode { get; set; }
        public string districtCode { get; set; }
        public required Guid projectPlanId { get; set; }
        public required string userId { get; set; }
    }
}

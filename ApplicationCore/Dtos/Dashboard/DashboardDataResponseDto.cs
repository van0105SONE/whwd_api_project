using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Dashboard
{
    public class DashboardDataResponseDto
    {

        public double totalFund { get; set; }
        public double totalRecieve { get; set; }
        public double remainingDate { get; set; }

        public int fundraisedPlace { get; set; }

         public List<SchoolSerieResponseDto>  schools { get; set; }
    }
}

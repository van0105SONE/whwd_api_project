using ApplicationCore.Dtos.Donate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Reports
{
    public class DonationReport
    {
        public int totalDonation { get; set; }
        public int totalOnline { get; set; }
        public int totalOffline { get; set; }
        public int totalCash { get; set; }
        public int totalThing { get; set; }
        public List<DonationResponseDto> donations { get; set; }
    }
}

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
        public int totalOrganiztion { get; set; }
        public int totalCompany { get; set; }
        public int totalPersonal { get; set; }
        public List<DonationResponseDto> donations { get; set; }
    }
}

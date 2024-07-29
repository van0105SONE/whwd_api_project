using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Donate
{
    public class DonationResponseDto
    {
        public string Title { get; set; }
        public string SourceType { get; set; }
        public string DonationType { get; set; }
        public double amount { get; set; }
        public DonatorResponseDto DonorBy { get; set; }
    }
}

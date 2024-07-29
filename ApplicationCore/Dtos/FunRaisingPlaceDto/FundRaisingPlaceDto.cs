using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using whwd_web_api.Dtos.Address;

namespace ApplicationCore.Dtos.FunRaisingPlaceDto
{
    public class FundRaisingPlaceDto
    {
        public required string placeName { get; set; }
        public string? phoneNumber { get; set; }
        public string? email { get; set; }
        public string? other { get; set; }
        public string? facebook { get; set; }
        public string? googleMapLink { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public required string userId { get; set; }
        public required string coordinatorId { get; set; }
        public VillageDto village { get; set; }
    }



}

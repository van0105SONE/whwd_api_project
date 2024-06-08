using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.FunRaisingPlaceDto
{
    public class FundRaisingPlaceDto
    {
        public required string PlaceName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Other { get; set; }
        public string? Facebook { get; set; }
        public double Longtitude { get; set; } = 0;
        public double Latitude { get; set; } = 0;
        public required string userId { get; set; }
        public required string coordinatorId { get; set; }
        public required string  villageCode { get; set; }
    }



}

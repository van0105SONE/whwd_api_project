using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Address
{
    public class DistrictResponseDto
    {
        public String districtCode { get; set; }
        public String districtName { get; set; }
        public ProvinceResponseDto province { get; set; }
    }
}

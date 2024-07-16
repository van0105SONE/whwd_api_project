using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Address
{
    public class VillageReponseDto
    {
        public string? villageCode { get; set; }
        public required string villageName { get; set; }
        public required DistrictResponseDto district { get; set; }
    }
}

using ApplicationCore.Dtos.FunRaisingPlaceDto;
using Infrastructure.Model.Place;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Reports
{
    public class PlaceReport
    {
        public int totalAcceptPlace { get; set; }
        public int totalRejectPlace { get; set; }

        public int totalOnProgress { get; set; }    

        public List<PlaceResponseDto>? places { get; set; }
    }
}

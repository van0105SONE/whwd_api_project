using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.FunRaisingPlaceDto
{
    public class PlaceUpdateStatusDto
    {
        public DateTime? startDate {get; set;}
        public DateTime? endDate { get; set;}    
        public string? status { get; set; }

        public string? userId { get; set; }
        
    }
}

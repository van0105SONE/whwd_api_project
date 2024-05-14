using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Address
{
    public class District
    {
        [Key]
        public String districtCode { get; set; }
        public String districtName { get; set;}
        public Province province { get; set; }
    }
}

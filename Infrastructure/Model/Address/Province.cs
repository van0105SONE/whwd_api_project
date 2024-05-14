using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Address
{
    public class Province
    {
        [Key]
        public String ProvinceCode { get; set; }
        public String ProvinceName { get; set; }
    }
}

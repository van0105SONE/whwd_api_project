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
        public required String ProvinceCode { get; set; }
        public required String ProvinceName { get; set; }
    }
}

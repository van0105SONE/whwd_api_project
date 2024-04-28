using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Volunteer
{
    public class Generation 
    {
        [Key]
        public String ReferenceCode { get; set; }
        public int number { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.University
{
    public class University
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

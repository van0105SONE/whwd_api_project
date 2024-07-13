using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.University
{
    public class Major
    {
        public string? Id { get; set; }
        public string Name { get; set; }

        public Department Department { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.University
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Faculty faculty { get; set; }

    }
}

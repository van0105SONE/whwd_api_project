using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Work
{
    public class Faculty
    {
        public Guid Id { get; set; }
        public String Name { get; set; }

        public Department Department { get; set; }
    }
}

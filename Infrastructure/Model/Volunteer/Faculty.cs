using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Volunteer
{
    public class Faculty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Department Department { get; set; }
    }
}

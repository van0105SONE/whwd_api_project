using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Work
{
    public class Major
    {
        public Guid Id { get; set; }
        public String Name { get; set; }

        public  Faculty Faculty { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.University
{
    public class Faculty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public University university { get; set; }
    }
}

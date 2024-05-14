using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Filter
{
    public class BaseFilter
    {
        public int page { get; set; }   
        public int pageSize { get; set; }   
        public string keywords { get; set; }
    }
}

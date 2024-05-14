using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Users
{
    public class Position
    {
        public  Guid Id { get; set; }
        public  string RefNo { get; set; }
        public string PositionName { get; set; }
    }
}

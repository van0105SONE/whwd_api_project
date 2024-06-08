using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Donate
{
    public class Donator : BaseModel
    {
        public string? Name { get; set; }
        public string? SponsorType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Facebook { get; set; }
    }
}

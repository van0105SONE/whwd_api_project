using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Work
{
    public class DonateThing : BaseModel
    {
        public String Name { get; set; }
        public Double Price { get; set;  }

        public string UnitType { get; set; }
        public int Unit { get; set; }
        public int personAmount { get; set; }
        public ProjectPlan ProjectPlan { get; set; } 
    }
}

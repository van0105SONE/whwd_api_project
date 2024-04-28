using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Work
{
    public class DonateThing : BaseModel
    {
        public String Name { get; set; }
        public Double Price { get; set;  }
        
    }
}

using Infrastructure.Model.Recipient;
using Infrastructure.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Student
{
    public class Recipient : BaseModel
    {
        public required string fname { get; set; }
        public required string lname {get; set;}
        public DateTime birthDate { get; set; }
        public required string level { get; set; }
        public string shirtSize { get; set; }
        public string? skirtSize { get; set; }
        public string? shoesSize { get; set; }
        public School School { get; set; }
        public required ProjectPlan Project { get; set; }
    }
}

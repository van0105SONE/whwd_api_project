using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Student
{
    public class Student : BaseModel
    {
        public String Name { get; set; }
        public DateTime birthDate { get; set; }
        public String Year { get; set; }

        public int ShirtSize { get; set; }
        public int SkirtSize { get; set; }
        public int ShoesSize { get; set; }
    }
}

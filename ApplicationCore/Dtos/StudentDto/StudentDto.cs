using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.StudentDto
{
    public class StudentDto
    {
        public required string fname { get; set; }
        public required string lname {get; set;}
        public DateTime birthDate { get; set; }
        public required string level { get; set; }
        public int ShirtSize { get; set; }
        public int SkirtSize { get; set; }
        public int ShoesSize { get; set; }
        public int GloveSize { get; set; }
        public  required string userId { get; set; }
    }

    public class StudentUpdateDto : StudentDto {
        public required Guid Id {get; set;}
    }
}

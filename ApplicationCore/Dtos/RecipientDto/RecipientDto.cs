using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.StudentDto
{
    public class RecipientDto
    {
        public required string fname { get; set; }
        public required string lname {get; set;}
        public DateTime birthDate { get; set; }
        public required string level { get; set; }
        public int shouldSize { get; set; }
        public int chestSize { get; set; }
        public int bodyLength { get; set; }
        public int hemSize { get; set; }
        public  required string userId { get; set; }
    }

    public class StudentUpdateDto : RecipientDto {
        public required Guid Id {get; set;}
    }
}

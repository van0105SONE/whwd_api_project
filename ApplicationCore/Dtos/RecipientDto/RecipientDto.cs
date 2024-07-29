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
        public  string? gender { get; set; }
        public DateTime birthDate { get; set; }
        public required string level { get; set; }
        public string shirtSize { get; set; }
        public string? skirtSize { get; set; }
        public string? shoesSize { get; set; }
        public Guid schoolId { get; set; }
        public  required string userId { get; set; }
    }

}

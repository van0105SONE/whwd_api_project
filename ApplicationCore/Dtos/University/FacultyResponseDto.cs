using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.University
{
    public class FacultyResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UniversityResponseDto university { get; set; }
    }
}

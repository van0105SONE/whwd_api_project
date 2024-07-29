using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.University
{
    public class DepartmentResponseDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public FacultyResponseDto faculty { get; set; }
    }
}

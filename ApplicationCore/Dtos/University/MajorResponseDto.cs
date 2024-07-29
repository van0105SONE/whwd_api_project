using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.University
{
    public class MajorResponseDto
    {
        public required string id { get; set; }
        public required string name { get; set; }

        public DepartmentResponseDto department { get; set; }

    }
}

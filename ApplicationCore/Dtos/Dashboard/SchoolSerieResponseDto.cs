using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Dashboard
{
    public class SchoolSerieResponseDto
    {
        public required string Name { get; set; }
        public int totalFund { get; set; }
    }
}

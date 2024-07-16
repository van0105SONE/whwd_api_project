using ApplicationCore.Dtos.RecipientDto;
using ApplicationCore.Dtos.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Recipient
{
    public class RecipientReponseDto
    {
        public required string fname { get; set; }
        public required string lname { get; set; }
        public DateTime birthDate { get; set; }
        public required string level { get; set; }
        public string shirtSize { get; set; }
        public string? skirtSize { get; set; }
        public string? shoesSize { get; set; }
        public SchoolResponseDto School { get; set; }
        public required ProjectPlanResponseDto Project { get; set; }
    }
}

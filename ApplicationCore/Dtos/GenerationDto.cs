using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Dtos
{
    public class GenerationDto
    {
        [Key]
        public String ReferenceCode { get; set; }
        public int number { get; set; }

        public DateTime YearStart { get; set; }
        public DateTime YearEnd { get; set; }
    }
}

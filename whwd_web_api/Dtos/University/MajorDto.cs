
namespace whwd_web_api.Dtos.University
{
    public class MajorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public DepartmentDto Department { get; set; }
    }
}

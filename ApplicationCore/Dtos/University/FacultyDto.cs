namespace ApplicationCore.Dtos.University
{
    public class FacultyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UniversityDto university { get; set; }
    }
}

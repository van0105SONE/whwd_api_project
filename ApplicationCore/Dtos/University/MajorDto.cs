namespace ApplicationCore.Dtos.University
{
    public class MajorDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public required Guid DepartmentId { get; set; }
    }
}

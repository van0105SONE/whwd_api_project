namespace ApplicationCore.Dtos.University
{
    public class MajorDto
    {
        public required string id { get; set; }
        public required string name { get; set; }

        public required Guid departmentId { get; set; }

    }
}

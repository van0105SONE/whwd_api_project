using Infrastructure.Model.Work;

namespace whwd_web_api.Dtos.Work
{
    public class ProjectPlanDto
    {
        public String ProjectName { get; set; }
        public String Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DonateThing DonateThing { get; set; }
      
    }
}

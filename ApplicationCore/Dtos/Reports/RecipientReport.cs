

using ApplicationCore.Dtos.Recipient;

namespace ApplicationCore.Dtos.Reports
{
    public class RecipientReport
    {
        public int totalRecipient { get; set; }
        public List<RecipientBySchool> totalRecipientBySchool {get;set;}
        public List<RecipientReponseDto> recipients {get; set;}
    }
}

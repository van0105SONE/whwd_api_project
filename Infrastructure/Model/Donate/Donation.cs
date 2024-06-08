
using Infrastructure.Model.Account;
using System.Transactions;

namespace Infrastructure.Model.Donate
{
    public class Donation : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DonationType { get; set; }
        public double amount { get; set; }
		public required SourceType SourceTypes { get; set; }
		public Donator DonorBy { get; set; }
    }
}

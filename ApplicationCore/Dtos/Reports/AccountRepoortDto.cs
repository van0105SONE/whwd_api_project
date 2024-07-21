
using ApplicationCore.Dtos.TransactionDto;

namespace ApplicationCore.Dtos.Reports
{
    public class AccountRepoortDto
    {
        public double totalExpense { get; set; }
        public double totalIncome { get; set; }
        public int totalTransaction { get; set; }

        public double  totalDonation { get; set; }

        public List<TransactionResponseDto> transactions { get; set; }
    }
}

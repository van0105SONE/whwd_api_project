
using ApplicationCore.Dtos.TransactionDto;

namespace ApplicationCore.Dtos.Reports
{
    public class AccountRepoortDto
    {
        public decimal totalExpense { get; set; }
        public decimal totalIncome { get; set; }

        public string  totalDonation { get; set; }

        public List<TransactionResponseDto> transactions { get; set; }
    }
}

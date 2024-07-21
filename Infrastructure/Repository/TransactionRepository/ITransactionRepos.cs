using ApplicationCore.Filter;
using ApplicationCore.Filter.report;
using ErrorOr;
using Infrastructure.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.TransactionRepository
{
	public interface ITransactionRepos
	{
		public Task<ErrorOr<bool>> createTransaction(Transaction transactionParam);
        public Task<ErrorOr<bool>> deleteTransaction(Guid Id);

		public Task<List<Transaction>> getTransactions(BaseFilter filter);
        public Task<Transaction> getTransactionId(Guid Id);

        public Task<List<Transaction>> getTransactionsReport(ReportAccountFilter filter);

        public Task<double> getTotalIncome();
        public Task<double> getTotalExpense();
        public Task<double> getTotalDonation();

        public Task<int> getTotalTransaction();


    }
}

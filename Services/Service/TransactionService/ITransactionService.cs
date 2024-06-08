using ApplicationCore.Dtos.TransactionDto;
using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.TransactionService
{
	public interface ITransactionService
	{
		public Task<ErrorOr<bool>> createTransaction(TransactionDto transactionParam);
		public Task<ErrorOr<bool>> deleteTransaction(Guid Id);

		public Task<List<Transaction>> getTransactions(BaseFilter filter);
		public Task<Transaction> getTransactionId(Guid Id);
		public Task<List<TransactionType>> getTransactionTypes();

	}
}

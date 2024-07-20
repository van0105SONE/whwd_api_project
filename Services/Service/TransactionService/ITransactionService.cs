using ApplicationCore.Dtos;
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
		public Task<ErrorOr<MessageReponse<TransactionResponseDto>>> createTransaction(TransactionDto transactionParam);
		public Task<ErrorOr<MessageReponse<TransactionResponseDto>>> deleteTransaction(Guid Id);

		public Task<MessageReponse<List<TransactionResponseDto>>> getTransactions(BaseFilter filter);
		public Task<MessageReponse<TransactionResponseDto>> getTransactionId(Guid Id);

	}
}

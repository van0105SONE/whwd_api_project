using ApplicationCore.Filter;
using ApplicationCore.Filter.report;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.TransactionRepository
{
	public class TransactionRepository : ITransactionRepos
	{
		private DatabaseContexts  _DbContexts;
		public TransactionRepository(DatabaseContexts context) { 
		   _DbContexts = context;
		}

		public async Task<ErrorOr<bool>> createTransaction(Transaction transactionParam)
		{
			try
			{
				_DbContexts.transactions.Add(transactionParam);
				_DbContexts.SaveChanges();
				return true;
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public  async Task<ErrorOr<bool>> deleteTransaction(Guid Id)
		{
			try
			{
			    Transaction? transaction =	_DbContexts.transactions.FirstOrDefault(t => t.Id == Id);

				_DbContexts.Remove(transaction);
				_DbContexts.SaveChanges();
				return true;
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}



		public async Task<Transaction> getTransactionId(Guid Id)
		{
			try
			{

				return _DbContexts.transactions.FirstOrDefault(t => t.Id == Id);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<Transaction>> getTransactions(BaseFilter filter)
		{
			try
			{

				var list = _DbContexts.transactions.Skip(((filter.page - 1) * filter.pageSize)).Take(filter.pageSize).ToList();
				return list;
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        public async Task<List<Transaction>> getTransactionsReport(ReportAccountFilter filter)
        {
			try
			{
			  return  _DbContexts.transactions.Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize).Where(t => t.CreateAt.Date >= filter.startDate && t.CreateAt.Date <= filter.endDate).ToList();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
        }
    }
}

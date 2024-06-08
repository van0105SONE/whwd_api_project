using ApplicationCore.Filter;
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

				return _DbContexts.transactions.Take(((filter.page - 1) * filter.pageSize)).Take(filter.pageSize).ToList();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<TransactionType> getTransactionTypeById(Guid Id)
		{
			try
			{
				return _DbContexts.transactionTypes.FirstOrDefault(t => t.Id == Id);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<TransactionType> getTransactionTypeByName(string typeName)
		{
			try
			{
				return _DbContexts.transactionTypes.First(t => t.Name.ToUpper() == typeName.ToUpper());
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<TransactionType>> getTransactionTypes()
		{
			try
			{
		       return	_DbContexts.transactionTypes.ToList();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}

using ApplicationCore.Filter;
using ApplicationCore.Filter.report;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
using Microsoft.EntityFrameworkCore;
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

        public async Task<double> getTotalDonation()
        {
            try
            {
                return _DbContexts.transactions.Where(t => t.TransactionType.ToUpper() == "DONATION").Sum(t => t.Amount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<double> getTotalExpense()
        {
            try
            {
                return _DbContexts.transactions.Where(t => t.TransactionType.ToUpper() == "EXPENSE").Sum(t => t.Amount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<double> getTotalIncome()
        {
			try
			{
				return _DbContexts.transactions.Where(t => t.TransactionType.ToUpper() == "INCOME").Sum(t => t.Amount);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
        }

        public async Task<int> getTotalTransaction()
        {
            try
            {
                return _DbContexts.transactions.Count();
            }
            catch (Exception ex)
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
				var list = _DbContexts.transactions.Include(t => t.Account).Include(t => t.CreateBy).Skip(((filter.page - 1) * filter.pageSize)).Take(filter.pageSize).ToList();
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
			  return  _DbContexts.transactions.Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize).Where(t => t.CreateAt.Date >= filter.startDate.Value.Date && t.CreateAt.Date <= filter.endDate.Value.Date).ToList();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
        }
    }
}

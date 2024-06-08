using ApplicationCore.Dtos.TransactionDto;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
using Infrastructure.Model.Users;
using Infrastructure.Repository.AccountRepos;
using Infrastructure.Repository.ProjectRepository;
using Infrastructure.Repository.TransactionRepository;
using Microsoft.AspNetCore.Identity;

namespace Services.Service.TransactionService
{
	public class TransactionService : ITransactionService
	{
		IMapper _Mapper { get; set; }
		private UserManager<ApplicationUser> _userManager { get; set; }
		private ITransactionRepos _transactionRepos { get; set; }
	    private IProjectPlanRepository _projectPlanRepository { get; set; }
		private IAccountRepository _accountRepository { get; set; }
		public TransactionService(UserManager<ApplicationUser> UserManager, DatabaseContexts contexts, IMapper mapper) { 
		     _Mapper = mapper;
			 _userManager = UserManager;
			 _transactionRepos = new TransactionRepository(contexts);
			_projectPlanRepository = new ProjectPlanRepository(contexts);
			_accountRepository = new AccountRepository(contexts);
		}
		public async Task<ErrorOr<bool>> createTransaction(TransactionDto transactionParam)
		{
			try
			{
			    Transaction transaction =	_Mapper.Map<Transaction>(transactionParam);
				ApplicationUser? user = await _userManager.FindByIdAsync(transactionParam.UserId);
				if (user == null)
				{
					return Error.NotFound("User not found, required user id");
				}


		        Account account =	await	_accountRepository.getAccountById(transactionParam.AccountId);
				if (account == null)
				{
					return Error.NotFound("Account not found, required user id");
				}


				TransactionType transactionType = await _transactionRepos.getTransactionTypeById(transactionParam.TransactionTypeId);

				if (transactionType == null)
				{
					return Error.NotFound("transaction type not found, required transaction type id");
				}


				transaction.CreateBy = user;
				transaction.TransactionType = transactionType;
				transaction.Account = account;

				return await _transactionRepos.createTransaction(transaction);
			}catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		Task<List<Transaction>> ITransactionService.getTransactions(BaseFilter filter)
		{
			try
			{
				return _transactionRepos.getTransactions(filter);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<ErrorOr<bool>> deleteTransaction(Guid Id)
		{
			try
			{
			  return await	_transactionRepos.deleteTransaction(Id);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Transaction> getTransactionId(Guid Id)
		{
			try
			{
				return await _transactionRepos.getTransactionId(Id);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


		public async Task<List<TransactionType>> getTransactionTypes()
		{
			try
			{
				return await _transactionRepos.getTransactionTypes();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

	}
}

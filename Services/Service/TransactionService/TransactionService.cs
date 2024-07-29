using ApplicationCore.Constanst;
using ApplicationCore.Dtos;
using ApplicationCore.Dtos.TransactionDto;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
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
        private DatabaseContexts _dbContexts { get; set; }

		public TransactionService(UserManager<ApplicationUser> UserManager, DatabaseContexts contexts, IMapper mapper) { 
		     _Mapper = mapper;
			 _userManager = UserManager;
			 _transactionRepos = new TransactionRepository(contexts);
			_projectPlanRepository = new ProjectPlanRepository(contexts);
			_accountRepository = new AccountRepository(contexts);
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



       public async Task<ErrorOr<MessageReponse<TransactionResponseDto>>> createTransaction(TransactionDto transactionParam)
        {
            try
            {
                Transaction transaction = _Mapper.Map<Transaction>(transactionParam);
                ApplicationUser? user = await _userManager.FindByIdAsync(transactionParam.userId);
                if (user == null)
                {
                    return Error.NotFound(ErrorCodes.Validation,"User not found, required user id");
                }


                Account account = await _accountRepository.getAccountById(transactionParam.accountId);
                if (account == null)
                {
                    return Error.NotFound(ErrorCodes.Validation, "Account not found, required user id");
                }
                transaction.CreateBy = user;
                transaction.Account = account;
                var result = await _transactionRepos.createTransaction(transaction);

                if (result.Value)
                {
                    Account mainAccount = _dbContexts.accounts.FirstOrDefault(t => t.AccountTypes.ToUpper() == "MAIN" && t.ProjectPlan.IsActive);
                    mainAccount.Balance += transaction.Amount;

                    _dbContexts.accounts.Update(mainAccount);
                    _dbContexts.SaveChanges();

                    ProjectPlan projectPlan = _dbContexts.projectPlan.FirstOrDefault(t => t.IsActive);
                    projectPlan.TotalRecieve += transaction.Amount;
                    _dbContexts.projectPlan.Update(projectPlan);
                    _dbContexts.SaveChanges();
                    var response = _Mapper.Map<TransactionResponseDto>(transaction);
                    return new MessageReponse<TransactionResponseDto>()
                    {
                        isSuccess = true,
                        statusCode = 200,
                        message = "Successful",
                        data = response
                    };
                }
                else
                {
                    return new MessageReponse<TransactionResponseDto>()
                    {
                        isSuccess = false,
                        statusCode = 500,
                        message = "Faile",
                    };
                }




			}
			catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        Task<ErrorOr<MessageReponse<TransactionResponseDto>>> ITransactionService.deleteTransaction(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageReponse<List<TransactionResponseDto>>> getTransactions(BaseFilter filter)
        {
            try
            {
                var result = await _transactionRepos.getTransactions(filter);
                var response = _Mapper.Map<List<TransactionResponseDto>>(result);
                return new MessageReponse<List<TransactionResponseDto>>()
                {
                    isSuccess = true,
                    statusCode = 200,
                    message = "Successful",
                    data = response
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

      public async Task<MessageReponse<TransactionResponseDto>> getTransactionId(Guid Id)
        {
            try
            {
                var result = await _transactionRepos.getTransactionId(Id);
                var response = _Mapper.Map<TransactionResponseDto>(result);
                return new MessageReponse<TransactionResponseDto>()
                {
                    isSuccess = true,
                    statusCode = 200,
                    message = "Successful",
                    data = response
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

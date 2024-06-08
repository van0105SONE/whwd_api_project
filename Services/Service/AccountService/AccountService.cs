using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
using Infrastructure.Repository.AccountRepos;
using Infrastructure.Repository.ProjectRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using whwd_web_api.Dtos.Accounts;

namespace Services.Service.AccountService {
    public class AccountService : IAccountService
    {
       private IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;
       private IAccountRepository _accountRepository;
       private IProjectPlanRepository _projectPlanRepository;
        public AccountService(DatabaseContexts contexts, UserManager<ApplicationUser> userManager,IMapper mapper){
            _mapper = mapper;
           _accountRepository = new AccountRepository(contexts);
           _projectPlanRepository = new ProjectPlanRepository(contexts);
           
           _userManager = userManager;
        }
        public async Task<ErrorOr<bool>> createAccount(AccountDto accountDto)
        {
           try{
              Account account = _mapper.Map<Account>(accountDto);
              ApplicationUser? userCreate = await  _userManager.FindByIdAsync(accountDto.userId);
              ApplicationUser? userOwner = await _userManager.FindByIdAsync(accountDto.userId);
              var projectPlanResult = await _projectPlanRepository.getProjectActiveProject();
              ProjectPlan projectPlan =  projectPlanResult.Value;
              AccountType accountType = await _accountRepository.GetAccountTypeById(accountDto.AccountTypeId);
              account.ProjectPlan = projectPlan;
              account.OwnBy = userOwner;
              account.CreateBy = userCreate;
              account.AccountTypes = accountType;
              return   await _accountRepository.createAccount(account);
           }catch(Exception ex){
            throw new Exception("", ex);
           }
        }

        public async Task<ErrorOr<bool>> deleteAccount(Guid Id)
        {
            try{
                return await _accountRepository.deleteAccount(Id);
            }catch(Exception ex){
                throw new Exception(ex.Message);
            }
        }

		public Task<List<AccountType>> GetAccountTypes()
		{
            try
            {
				return _accountRepository.getAccountTypes();
			}
			catch(Exception ex)
            {
                throw new Exception(ex.Message);    
            }

		}

		async  public Task<List<Account>> GetAllAccounts(BaseFilter filter)
        {
            try{
            return await _accountRepository.GetAllAccounts(filter);
            }catch(Exception ex){
                throw new Exception(ex.Message);
            }
        }

       async public Task<ErrorOr<bool>> updateAccount(AccountUpdateDto accountDto)
        {
           try{
              Account accountMapper = _mapper.Map<Account>(accountDto);
              
               Account account = await _accountRepository.getAccountById(accountDto.Id);
              ApplicationUser? userCreate = await  _userManager.FindByIdAsync(accountDto.userId);
              ApplicationUser? userOwner = await _userManager.FindByIdAsync(accountDto.userId);
              var projectPlanResult = await _projectPlanRepository.getProjectActiveProject();
              ProjectPlan projectPlan =  projectPlanResult.Value;
              AccountType accountType = await _accountRepository.GetAccountTypeById(accountDto.AccountTypeId);
              account.AccountNo = accountMapper.AccountNo;
              account.ProjectPlan = projectPlan;
              account.OwnBy = userOwner;
              account.CreateBy = userCreate;
              account.AccountTypes = accountType;
            return await  _accountRepository.updateAccount(account);
           }catch(Exception ex){
              throw new Exception($"{ex.Message}");
           }
        }



    }
}
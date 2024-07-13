using ApplicationCore.Constanst;
using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Accounts;
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
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public async Task<ErrorOr<MessageReponse<AccountResponseDto>>> createAccount(AccountDto accountDto)
        {
           try{
              Account account = _mapper.Map<Account>(accountDto);

              ApplicationUser? userCreate = await  _userManager.FindByIdAsync(accountDto.userId);
              ApplicationUser? userOwner = await _userManager.FindByIdAsync(accountDto.userId);
              var projectPlanResult = await _projectPlanRepository.getProjectActiveProject();
              ProjectPlan projectPlan =  projectPlanResult.Value;

                if (projectPlan == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "Project plan isn't found, Did you create a new plan for this year already!");
                }else if (userOwner == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "Owner isn't found, Owner is is required");
                }else if (userCreate == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "Invalid user Data");
                }


              account.ProjectPlan = projectPlan;
              account.OwnBy = userOwner;
              account.CreateBy = userCreate;

              var isSuccess =  await _accountRepository.createAccount(account);
                if (isSuccess)
                {
                    var accountResponse = _mapper.Map<AccountResponseDto>(account);
                    return new MessageReponse<AccountResponseDto>() {
                        isSuccess = true,
                        message = "Successful",
                        data = accountResponse

                    };

                }
                else
                {
                    return new MessageReponse<AccountResponseDto>()
                    {
                        isSuccess = false,
                        message = "Fail to create account",


                    };
                }

            }
            catch(Exception ex){
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


		async  public Task<List<Account>> GetAllAccounts(BaseFilter filter)
        {
            try{
            return await _accountRepository.GetAllAccounts(filter);
            }catch(Exception ex){
                throw new Exception(ex.Message);
            }
        }

       async public Task<ErrorOr<MessageReponse<AccountResponseDto>>> updateAccount(Guid accId, AccountDto accountDto)
        {
           try{
              Account accountMapper = _mapper.Map<Account>(accountDto);
              
               Account account = await _accountRepository.getAccountById(accId);
              ApplicationUser? userCreate = await  _userManager.FindByIdAsync(accountDto.userId);
              ApplicationUser? userOwner = await _userManager.FindByIdAsync(accountDto.userId);
              var projectPlanResult = await _projectPlanRepository.getProjectActiveProject();
              ProjectPlan projectPlan =  projectPlanResult.Value;
                if (account == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "Account isn't found, Account Id is required");
                }
                else if (userOwner == null)
                {
                    account.OwnBy = userOwner;
                }
                else if (userCreate == null)
                {
                    account.CreateBy = userCreate;
                }

                account.AccountTypes = accountDto.AccountTypes;
                account.AccountNo = accountMapper.AccountNo;

  
             var isSuccessful = await  _accountRepository.updateAccount(account);
                if (isSuccessful)
                {
                    var accountResponse = _mapper.Map<AccountResponseDto>(account);
                    return new MessageReponse<AccountResponseDto>()
                    {
                        isSuccess = true,
                        message = "Successful",
                        data = accountResponse

                    };
                }
                else
                {

                    return new MessageReponse<AccountResponseDto>()
                    {
                        isSuccess = false,
                        message = "Fail to update account data",
                    };
                }
           }catch(Exception ex){
              throw new Exception($"{ex.Message}");
           }
        }



    }
}
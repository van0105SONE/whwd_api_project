using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Account;
using whwd_web_api.Dtos.Accounts;

namespace Services.Service.AccountService {
    public interface IAccountService {
     public Task<ErrorOr<bool>> createAccount(AccountDto accountDto);
     public Task<ErrorOr<bool>> updateAccount(AccountUpdateDto accountDto);
     public Task<ErrorOr<bool>> deleteAccount(Guid Id);
     public Task<List<Account>> GetAllAccounts(BaseFilter filter);

    }
}
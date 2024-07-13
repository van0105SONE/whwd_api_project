using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Accounts;
using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Account;
using whwd_web_api.Dtos.Accounts;

namespace Services.Service.AccountService {
    public interface IAccountService {
        public Task<ErrorOr<MessageReponse<AccountResponseDto>>> createAccount(AccountDto accountDto);
    public Task<ErrorOr<MessageReponse<AccountResponseDto>>> updateAccount(Guid accId, AccountDto accountDto);
     public Task<ErrorOr<bool>> deleteAccount(Guid Id);
     public Task<List<Account>> GetAllAccounts(BaseFilter filter);

    }
}
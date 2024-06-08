using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Account;

namespace Infrastructure.Repository.AccountRepos{
  public interface IAccountRepository {
     public Task<bool> createAccount(Account account);
     public Task<bool> updateAccount(Account account);
     public Task<bool> deleteAccount(Guid Id);

     public Task<Account>  getAccountById(Guid Id);
     public Task<List<Account>> GetAllAccounts(BaseFilter filter);

     public Task<AccountType> GetAccountTypeById (Guid Id);
     public Task<List<AccountType>> getAccountTypes();
  }
}
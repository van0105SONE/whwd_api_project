
using ApplicationCore.Filter;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.AccountRepos
{
    public class AccountRepository : IAccountRepository
    {
        private DatabaseContexts _DbContext { get; set; }
        public AccountRepository(DatabaseContexts contexts)
        {
            _DbContext = contexts;
        }
        async public Task<bool> createAccount(Account account)
        {
            try
            {
                await _DbContext.accounts.AddAsync(account);
                await _DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        async public Task<bool> deleteAccount(Guid Id)
        {
            try
            {
                Account account = _DbContext.accounts.First(t => t.Id == Id);
                _DbContext.accounts.Remove(account);
                await _DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        async public Task<List<Account>> GetAllAccounts(BaseFilter filter)
        {
            try
            {
                return await _DbContext.accounts.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        async public Task<bool> updateAccount(Account account)
        {
            try
            {
                _DbContext.accounts.Update(account);
                await _DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AccountType> GetAccountTypeById(Guid Id)
        {
           try{
            return await  _DbContext.accountTypes.FirstOrDefaultAsync(t => t.Id == Id);
           }catch(Exception ex){
            throw new Exception(ex.Message);    
           }
        }

        public Account getAccountById(Guid Id)
        {
         try{
              return _DbContext.accounts.FirstOrDefault(t => t.Id == Id);
         }catch(Exception ex){
            throw new Exception(ex.Message);
         }
        }
    }
}
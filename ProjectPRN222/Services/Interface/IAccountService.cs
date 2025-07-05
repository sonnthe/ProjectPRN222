using ProjectPRN222.Models;

namespace ProjectPRN222.Services.Interface
{
    public interface IAccountService
    {
        Account? GetAccountByEmailAndPassword(string email, string password);
        void Register(Account account);
        List<Account> GetAllAccounts();
        List<Account> GetAllAdmin();
        int GetAllAccountCount();   
    }
}

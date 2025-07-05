using ProjectPRN222.Models;

namespace ProjectPRN222.DataAccessLayers.Interface
{
    public interface IAccountDA
    {
        Account? GetAccountByEmailAndPassword(string email, string password);
        void Register(Account account);
        List<Account> GetAllAccounts();
        List<Account> GetAllAdmin();
        int GetAllAccountCount();
    }
}

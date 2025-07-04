using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Interface;

namespace ProjectPRN222.Services.Impl
{
    public class AccountService(IAccountDA accountDA) : IAccountService
    {
        private readonly IAccountDA _accountDA = accountDA;

        public Account? GetAccountByEmailAndPassword(string email, string password)
        {
            return _accountDA.GetAccountByEmailAndPassword(email, password);
        }

        public List<Account> GetAllAccounts()
        {
            return _accountDA.GetAllAccounts();
        }

        public List<Account> GetAllAdmin()
        {
            return _accountDA.GetAllAdmin();
        }

        public void Register(Account account)
        {
            _accountDA.Register(account);
        }
    }
}

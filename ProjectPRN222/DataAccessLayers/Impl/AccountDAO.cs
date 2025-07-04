using Microsoft.EntityFrameworkCore;
using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Models;

namespace ProjectPRN222.DataAccessLayers.Impl
{
    public class AccountDAO : IAccountDA
    {
        private QuizOnlineContext _context = new();

        public Account? GetAccountByEmailAndPassword(string email, string password)
        {
            return _context.Accounts.Include(a=>a.Role)
                .FirstOrDefault(a => a.Email == email && a.Password == password);
        }

        public List<Account> GetAllAccounts()
        {
            return _context.Accounts.Include(a => a.Role).ToList();
        }

        public List<Account> GetAllAdmin()
        {
            return _context.Accounts
                .Include(a => a.Role)
                .Where(a => a.Role.RoleName == "Admin")
                .ToList();
        }

        public void Register(Account account)
        {
            try
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while registering the account.", ex);
            }
        }
    }
}

using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly List<BankAccount> _accounts = new();

        public BankAccount CreateAccount(string name, decimal initialBalance)
        {
            var account = new BankAccount(name, initialBalance);
            _accounts.Add(account);
            return account;
        }

        public BankAccount? GetAccountById(Guid id) =>
            _accounts.FirstOrDefault(a => a.Id == id);

        public void DeleteAccount(Guid id) =>
            _accounts.RemoveAll(a => a.Id == id);

        public IEnumerable<BankAccount> GetAllAccounts() => _accounts;
    }
}

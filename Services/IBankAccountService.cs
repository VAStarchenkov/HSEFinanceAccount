using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public interface IBankAccountService
    {
        BankAccount CreateAccount(string name, decimal initialBalance);
        BankAccount? GetAccountById(Guid id);
        void DeleteAccount(Guid id);
        IEnumerable<BankAccount> GetAllAccounts();
    }
}

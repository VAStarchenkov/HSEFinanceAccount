namespace FinanceAccounting.Services
{
    public class ShowAccountsCommand : ICommand
    {
        private readonly IBankAccountService _accountService;

        public ShowAccountsCommand(IBankAccountService accountService)
        {
            _accountService = accountService;
        }

        public void Execute()
        {
            var accounts = _accountService.GetAllAccounts().ToList();
            if (!accounts.Any())
            {
                Console.WriteLine("Счетов пока нет.");
                return;
            }

            foreach (var account in accounts)
            {
                Console.WriteLine($"[{account.Id}] {account.Name} - Баланс: {account.Balance}");
            }
        }
    }
}

namespace FinanceAccounting.Services
{
    public class RecalculateBalanceCommand : ICommand
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly IBankAccountService _accountService;

        public RecalculateBalanceCommand(IAnalyticsService analyticsService, IBankAccountService accountService)
        {
            _analyticsService = analyticsService;
            _accountService = accountService;
        }

        public void Execute()
        {
            var accounts = _accountService.GetAllAccounts().ToList();
            if (!accounts.Any())
            {
                Console.WriteLine("Нет доступных счетов.");
                return;
            }

            Console.WriteLine("Выберите счет для пересчета баланса:");
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {accounts[i].Name} (Баланс: {accounts[i].Balance})");
            }

            if (!int.TryParse(Console.ReadLine(), out int accIndex) || accIndex < 1 || accIndex > accounts.Count)
            {
                Console.WriteLine("Некорректный выбор счета!");
                return;
            }

            var account = accounts[accIndex - 1];
            _analyticsService.RecalculateBalance(account.Id);
        }
    }
}

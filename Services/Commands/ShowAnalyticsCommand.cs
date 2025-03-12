namespace FinanceAccounting.Services
{
    public class ShowAnalyticsCommand : ICommand
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly IBankAccountService _accountService;

        public ShowAnalyticsCommand(IAnalyticsService analyticsService, IBankAccountService accountService)
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

            Console.WriteLine("Выберите счет:");
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

            Console.Write("Введите начальную дату (ГГГГ-ММ-ДД): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Некорректная дата!");
                return;
            }

            Console.Write("Введите конечную дату (ГГГГ-ММ-ДД): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                Console.WriteLine("Некорректная дата!");
                return;
            }

            var difference = _analyticsService.GetIncomeExpenseDifference(account.Id, startDate, endDate);
            Console.WriteLine($"Разница доходов и расходов за период: {difference}");

            var categorySummary = _analyticsService.GetCategorySummary(account.Id, startDate, endDate);
            Console.WriteLine("\nГруппировка по категориям:");
            foreach (var (category, total) in categorySummary)
            {
                Console.WriteLine($"{category}: {total}");
            }
        }
    }
}

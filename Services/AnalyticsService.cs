using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IBankAccountService _accountService;
        private readonly IOperationService _operationService;

        public AnalyticsService(IBankAccountService accountService, IOperationService operationService)
        {
            _accountService = accountService;
            _operationService = operationService;
        }

        public decimal GetIncomeExpenseDifference(Guid accountId, DateTime startDate, DateTime endDate)
        {
            var operations = _operationService.GetOperationsByAccount(accountId)
                .Where(o => o.Date >= startDate && o.Date <= endDate);

            return operations.Sum(o => o.Type == OperationType.Income ? o.Amount : -o.Amount);
        }

        public Dictionary<string, decimal> GetCategorySummary(Guid accountId, DateTime startDate, DateTime endDate)
        {
            var operations = _operationService.GetOperationsByAccount(accountId)
                .Where(o => o.Date >= startDate && o.Date <= endDate);

            return operations.GroupBy(o => o.CategoryId)
                .ToDictionary(g => _accountService.GetAccountById(accountId)?.Name ?? "Неизвестно", g => g.Sum(o => o.Amount));
        }

        public void RecalculateBalance(Guid accountId)
        {
            var account = _accountService.GetAccountById(accountId);
            if (account == null)
            {
                Console.WriteLine("Счет не найден.");
                return;
            }

            // Пересчитываем баланс на основе всех операций
            var newBalance = _operationService.GetOperationsByAccount(accountId)
                .Sum(o => o.Type == OperationType.Income ? o.Amount : -o.Amount);

            if (account.Balance != newBalance)
            {
                Console.WriteLine($"Пересчитанный баланс: {newBalance}. Текущий баланс: {account.Balance}. Баланс обновлен.");
                account.UpdateBalance(newBalance - account.Balance);
            }
            else
            {
                Console.WriteLine("Баланс корректен. Изменений не требуется.");
            }
        }
    }
}

using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public class OperationService : IOperationService
    {
        private readonly List<Operation> _operations = new();
        private readonly IBankAccountService _accountService;

        public OperationService(IBankAccountService accountService)
        {
            _accountService = accountService;
        }

        public Operation CreateOperation(OperationType type, Guid accountId, decimal amount, DateTime date, Guid categoryId, string? description = null)
        {
            var account = _accountService.GetAccountById(accountId);
            if (account == null) throw new ArgumentException("Счет не найден.");

            var operation = new Operation(type, accountId, amount, date, categoryId, description);
            _operations.Add(operation);

            account.UpdateBalance(type == OperationType.Income ? amount : -amount);

            return operation;
        }

        public IEnumerable<Operation> GetOperationsByAccount(Guid accountId) =>
            _operations.Where(o => o.BankAccountId == accountId);

        public IEnumerable<Operation> GetOperationsByCategory(Guid categoryId) =>
            _operations.Where(o => o.CategoryId == categoryId);

        public decimal CalculateBalance(Guid accountId) =>
            GetOperationsByAccount(accountId).Sum(o => o.Type == OperationType.Income ? o.Amount : -o.Amount);
    }
}

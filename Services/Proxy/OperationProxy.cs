using FinanceAccounting.Domain;
using System.Collections.Concurrent;

namespace FinanceAccounting.Services.Proxy
{
    public class OperationProxy : IOperationService, ICacheProxy<Operation>
    {
        private readonly IOperationService _realService;
        private readonly ConcurrentDictionary<Guid, Operation> _cache = new();

        public OperationProxy(IOperationService realService)
        {
            _realService = realService;
        }

        public Operation CreateOperation(OperationType type, Guid accountId, decimal amount, DateTime date, Guid categoryId, string? description = null)
        {
            var operation = _realService.CreateOperation(type, accountId, amount, date, categoryId, description);
            _cache[operation.Id] = operation;
            return operation;
        }

        public IEnumerable<Operation> GetOperationsByAccount(Guid accountId)
        {
            return _cache.Values.Where(o => o.BankAccountId == accountId)
                   .Concat(_realService.GetOperationsByAccount(accountId));
        }

        public IEnumerable<Operation> GetOperationsByCategory(Guid categoryId)
        {
            return _cache.Values.Where(o => o.CategoryId == categoryId)
                   .Concat(_realService.GetOperationsByCategory(categoryId));
        }

        public decimal CalculateBalance(Guid accountId)
        {
            return _cache.Values.Where(o => o.BankAccountId == accountId)
                   .Sum(o => o.Type == OperationType.Income ? o.Amount : -o.Amount)
                   + _realService.CalculateBalance(accountId);
        }

        public IEnumerable<Operation> GetAll() => _cache.Values;
        public void Add(Operation item) => _cache[item.Id] = item;
        public void ClearCache() => _cache.Clear();
    }
}

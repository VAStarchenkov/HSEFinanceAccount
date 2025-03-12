using FinanceAccounting.Domain;
using System.Collections.Concurrent;

namespace FinanceAccounting.Services.Proxy
{
    public class BankAccountProxy : IBankAccountService, ICacheProxy<BankAccount>
    {
        private readonly IBankAccountService _realService;
        private readonly ConcurrentDictionary<Guid, BankAccount> _cache = new();

        public BankAccountProxy(IBankAccountService realService)
        {
            _realService = realService;
        }

        public BankAccount CreateAccount(string name, decimal initialBalance)
        {
            var account = _realService.CreateAccount(name, initialBalance);
            _cache[account.Id] = account;
            return account;
        }

        public BankAccount? GetAccountById(Guid id)
        {
            return _cache.TryGetValue(id, out var account) ? account : _realService.GetAccountById(id);
        }

        public void DeleteAccount(Guid id)
        {
            _realService.DeleteAccount(id);
            _cache.TryRemove(id, out _);
        }

        public IEnumerable<BankAccount> GetAllAccounts()
        {
            if (_cache.Count == 0)
            {
                var accounts = _realService.GetAllAccounts();
                foreach (var acc in accounts)
                {
                    _cache[acc.Id] = acc;
                }
            }
            return _cache.Values;
        }

        public IEnumerable<BankAccount> GetAll() => GetAllAccounts();
        public void Add(BankAccount item) => _cache[item.Id] = item;
        public void ClearCache() => _cache.Clear();
    }
}

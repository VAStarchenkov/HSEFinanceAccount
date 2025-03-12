namespace FinanceAccounting.Services.Proxy
{
    public interface ICacheProxy<T>
    {
        IEnumerable<T> GetAll();
        void Add(T item);
        void ClearCache();
    }
}

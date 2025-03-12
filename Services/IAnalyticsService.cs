using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public interface IAnalyticsService
    {
        decimal GetIncomeExpenseDifference(Guid accountId, DateTime startDate, DateTime endDate);
        Dictionary<string, decimal> GetCategorySummary(Guid accountId, DateTime startDate, DateTime endDate);
        void RecalculateBalance(Guid accountId);
    }
}

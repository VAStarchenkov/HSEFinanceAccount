using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public interface IOperationService
    {
        Operation CreateOperation(OperationType type, Guid accountId, decimal amount, DateTime date, Guid categoryId, string? description = null);
        IEnumerable<Operation> GetOperationsByAccount(Guid accountId);
        IEnumerable<Operation> GetOperationsByCategory(Guid categoryId);
        decimal CalculateBalance(Guid accountId);
    }
}

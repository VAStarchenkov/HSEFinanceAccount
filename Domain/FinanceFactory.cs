namespace FinanceAccounting.Domain
{
    public class FinanceFactory
    {
        public BankAccount CreateBankAccount(string name, decimal initialBalance) =>
            new BankAccount(name, initialBalance);

        public Category CreateCategory(string name, CategoryType type) =>
            new Category(name, type);

        public Operation CreateOperation(OperationType type, Guid bankAccountId, decimal amount, DateTime date, Guid categoryId, string? description = null) =>
            new Operation(type, bankAccountId, amount, date, categoryId, description);
    }
}

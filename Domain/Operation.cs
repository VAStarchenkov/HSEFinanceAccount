using FinanceAccounting.Services.Export;

namespace FinanceAccounting.Domain
{
    public class Operation
    {
        public Guid Id { get; } = Guid.NewGuid();
        public OperationType Type { get; }
        public Guid BankAccountId { get; }
        public decimal Amount { get; }
        public DateTime Date { get; }
        public string? Description { get; }
        public Guid CategoryId { get; }

        public Operation(OperationType type, Guid bankAccountId, decimal amount, DateTime date, Guid categoryId, string? description = null)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма операции должна быть положительной.");

            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            Description = description;
        }
        public void Accept(IExportVisitor visitor) => visitor.Visit(this);
    }
}

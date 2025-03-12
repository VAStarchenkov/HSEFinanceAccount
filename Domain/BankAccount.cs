using FinanceAccounting.Services.Export;

namespace FinanceAccounting.Domain
{
    public class BankAccount
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public decimal Balance { get; private set; }

        public BankAccount(string name, decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название счета не может быть пустым.");
            if (initialBalance < 0)
                throw new ArgumentException("Баланс не может быть отрицательным.");

            Name = name;
            Balance = initialBalance;
        }

        public void UpdateBalance(decimal amount) => Balance += amount;

        public void Accept(IExportVisitor visitor) => visitor.Visit(this);
    }
}

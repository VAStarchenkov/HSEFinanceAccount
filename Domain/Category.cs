using FinanceAccounting.Services.Export;

namespace FinanceAccounting.Domain
{
    public class Category
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public CategoryType Type { get; }

        public Category(string name, CategoryType type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название категории не может быть пустым.");

            Name = name;
            Type = type;
        }
        public void Accept(IExportVisitor visitor) => visitor.Visit(this);
    }
}

using FinanceAccounting.Domain;
using System.Text.Json;

namespace FinanceAccounting.Services.Export
{
    public class JsonExportVisitor : IExportVisitor
    {
        private readonly List<BankAccount> _accounts = new();
        private readonly List<Category> _categories = new();
        private readonly List<Operation> _operations = new();

        public void Visit(BankAccount account) => _accounts.Add(account);
        public void Visit(Category category) => _categories.Add(category);
        public void Visit(Operation operation) => _operations.Add(operation);

        public void ExportToFile(string filePath)
        {
            var data = new
            {
                Accounts = _accounts,
                Categories = _categories,
                Operations = _operations
            };

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);

            Console.WriteLine($"Данные экспортированы в {filePath} (JSON)");
        }
    }
}

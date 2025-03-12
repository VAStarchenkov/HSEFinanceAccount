using FinanceAccounting.Domain;
using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace FinanceAccounting.Services.Export
{
    public class CsvExportVisitor : IExportVisitor
    {
        private readonly List<BankAccount> _accounts = new();
        private readonly List<Category> _categories = new();
        private readonly List<Operation> _operations = new();

        public void Visit(BankAccount account) => _accounts.Add(account);
        public void Visit(Category category) => _categories.Add(category);
        public void Visit(Operation operation) => _operations.Add(operation);

        public void ExportToFile(string filePath)
        {
            using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
            using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));

            csv.WriteRecords(_accounts);
            csv.WriteRecords(_categories);
            csv.WriteRecords(_operations);

            Console.WriteLine($"Данные экспортированы в {filePath} (CSV)");
        }
    }
}

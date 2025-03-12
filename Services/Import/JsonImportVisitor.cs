using FinanceAccounting.Domain;
using System.Text.Json;

namespace FinanceAccounting.Services.Import
{
    public class JsonImportVisitor : IImportVisitor
    {
        public (List<BankAccount>, List<Category>, List<Operation>) ImportFromFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var data = JsonSerializer.Deserialize<ExportData>(json) ?? new ExportData();

            Console.WriteLine($"Данные импортированы из {filePath} (JSON)");
            return (data.Accounts, data.Categories, data.Operations);
        }
    }
}

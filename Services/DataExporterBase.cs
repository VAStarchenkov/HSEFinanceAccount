using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public abstract class DataExporterBase : IDataExporter
    {
        public void ExportData(string filePath, IEnumerable<BankAccount> accounts, IEnumerable<Category> categories, IEnumerable<Operation> operations)
        {
            var data = new ExportData
            {
                Accounts = accounts.ToList(),
                Categories = categories.ToList(),
                Operations = operations.ToList()
            };

            SaveToFile(filePath, data);
        }

        public (List<BankAccount>, List<Category>, List<Operation>) ImportData(string filePath)
        {
            var data = LoadFromFile(filePath);
            return (data.Accounts, data.Categories, data.Operations);
        }

        protected abstract void SaveToFile(string filePath, ExportData data);
        protected abstract ExportData LoadFromFile(string filePath);
    }

    public class ExportData
    {
        public List<BankAccount> Accounts { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Operation> Operations { get; set; } = new();
    }
}

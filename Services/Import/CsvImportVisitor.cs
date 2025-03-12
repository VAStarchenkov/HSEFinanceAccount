using FinanceAccounting.Domain;
using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace FinanceAccounting.Services.Import
{
    public class CsvImportVisitor : IImportVisitor
    {
        public (List<BankAccount>, List<Category>, List<Operation>) ImportFromFile(string filePath)
        {
            using var reader = new StreamReader(filePath, Encoding.UTF8);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            var accounts = csv.GetRecords<BankAccount>().ToList();
            var categories = csv.GetRecords<Category>().ToList();
            var operations = csv.GetRecords<Operation>().ToList();

            Console.WriteLine($"Данные импортированы из {filePath} (CSV)");
            return (accounts, categories, operations);
        }
    }
}

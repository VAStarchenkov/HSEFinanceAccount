using FinanceAccounting.Domain;
using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace FinanceAccounting.Services
{
    public class CsvDataExporter : DataExporterBase
    {
        protected override void SaveToFile(string filePath, ExportData data)
        {
            using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
            using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));

            csv.WriteRecords(data.Accounts);
            csv.WriteRecords(data.Categories);
            csv.WriteRecords(data.Operations);
        }

        protected override ExportData LoadFromFile(string filePath)
        {
            using var reader = new StreamReader(filePath, Encoding.UTF8);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            var accounts = csv.GetRecords<BankAccount>().ToList();
            var categories = csv.GetRecords<Category>().ToList();
            var operations = csv.GetRecords<Operation>().ToList();

            return new ExportData { Accounts = accounts, Categories = categories, Operations = operations };
        }
    }
}

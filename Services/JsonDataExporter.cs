using FinanceAccounting.Domain;
using System.Text.Json;

namespace FinanceAccounting.Services
{
    public class JsonDataExporter : DataExporterBase
    {
        protected override void SaveToFile(string filePath, ExportData data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        protected override ExportData LoadFromFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ExportData>(json) ?? new ExportData();
        }
    }
}

using FinanceAccounting.Domain;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FinanceAccounting.Services.Import
{
    public class YamlImportVisitor : IImportVisitor
    {
        public (List<BankAccount>, List<Category>, List<Operation>) ImportFromFile(string filePath)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var yaml = File.ReadAllText(filePath);
            var data = deserializer.Deserialize<ExportData>(yaml) ?? new ExportData();

            Console.WriteLine($"Данные импортированы из {filePath} (YAML)");
            return (data.Accounts, data.Categories, data.Operations);
        }
    }
}

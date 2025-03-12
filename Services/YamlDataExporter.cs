using FinanceAccounting.Domain;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FinanceAccounting.Services
{
    public class YamlDataExporter : DataExporterBase
    {
        protected override void SaveToFile(string filePath, ExportData data)
        {
            var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            var yaml = serializer.Serialize(data);
            File.WriteAllText(filePath, yaml);
        }

        protected override ExportData LoadFromFile(string filePath)
        {
            var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            var yaml = File.ReadAllText(filePath);
            return deserializer.Deserialize<ExportData>(yaml) ?? new ExportData();
        }
    }
}

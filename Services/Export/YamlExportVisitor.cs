using FinanceAccounting.Domain;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FinanceAccounting.Services.Export
{
    public class YamlExportVisitor : IExportVisitor
    {
        private readonly List<BankAccount> _accounts = new();
        private readonly List<Category> _categories = new();
        private readonly List<Operation> _operations = new();

        public void Visit(BankAccount account) => _accounts.Add(account);
        public void Visit(Category category) => _categories.Add(category);
        public void Visit(Operation operation) => _operations.Add(operation);

        public void ExportToFile(string filePath)
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var yaml = serializer.Serialize(new
            {
                Accounts = _accounts,
                Categories = _categories,
                Operations = _operations
            });

            File.WriteAllText(filePath, yaml);
            Console.WriteLine($"Данные экспортированы в {filePath} (YAML)");
        }
    }
}

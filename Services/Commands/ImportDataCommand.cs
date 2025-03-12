using FinanceAccounting.Services.Import;
using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public class ImportDataCommand : ICommand
    {
        private readonly IBankAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private readonly IOperationService _operationService;

        public ImportDataCommand(IBankAccountService accountService, 
                                 ICategoryService categoryService, 
                                 IOperationService operationService)
        {
            _accountService = accountService;
            _categoryService = categoryService;
            _operationService = operationService;
        }

        public void Execute()
        {
            Console.Write("Выберите формат (1 - JSON, 2 - CSV, 3 - YAML): ");
            var formatChoice = Console.ReadLine();

            IImportVisitor visitor = formatChoice switch
            {
                "2" => new CsvImportVisitor(),
                "3" => new YamlImportVisitor(),
                _   => new JsonImportVisitor()
            };

            Console.Write("Введите путь к файлу (например, import.json): ");
            var path = Console.ReadLine() ?? "import.json";

            var (accounts, categories, operations) = visitor.ImportFromFile(path);

            foreach (var account in accounts) _accountService.CreateAccount(account.Name, account.Balance);
            foreach (var category in categories) _categoryService.CreateCategory(category.Name, category.Type);
            foreach (var operation in operations) _operationService.CreateOperation(operation.Type, operation.BankAccountId, operation.Amount, operation.Date, operation.CategoryId, operation.Description);

            Console.WriteLine("Импорт завершен.");
        }
    }
}

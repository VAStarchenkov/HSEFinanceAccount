using FinanceAccounting.Services.Export;
using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public class ExportDataCommand : ICommand
    {
        private readonly IBankAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private readonly IOperationService _operationService;

        public ExportDataCommand(IBankAccountService accountService, 
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

            IExportVisitor visitor = formatChoice switch
            {
                "2" => new CsvExportVisitor(),
                "3" => new YamlExportVisitor(),
                _   => new JsonExportVisitor()
            };

            Console.Write("Введите путь для сохранения файла (например, export.json): ");
            var path = Console.ReadLine() ?? "export.json";

            foreach (var account in _accountService.GetAllAccounts())
                account.Accept(visitor);

            foreach (var category in _categoryService.GetAllCategories())
                category.Accept(visitor);

            foreach (var operation in _operationService.GetOperationsByAccount(Guid.Empty))
                operation.Accept(visitor);

            visitor.ExportToFile(path);
        }
    }
}

using FinanceAccounting.Services;

namespace FinanceAccounting.UI
{
    public class App
    {
        private readonly IBankAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private readonly IOperationService _operationService;
        private readonly IAnalyticsService _analyticsService;
        private readonly IDataExporter _dataExporter;

        public App(IBankAccountService accountService, ICategoryService categoryService,
                   IOperationService operationService, IAnalyticsService analyticsService,
                   IDataExporter dataExporter)
        {
            _accountService = accountService;
            _categoryService = categoryService;
            _operationService = operationService;
            _analyticsService = analyticsService;
            _dataExporter = dataExporter;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Создать счет");
                Console.WriteLine("2. Показать счета");
                Console.WriteLine("3. Добавить операцию");
                Console.WriteLine("4. Аналитика");
                Console.WriteLine("5. Экспорт данных");
                Console.WriteLine("6. Импорт данных");
                Console.WriteLine("7. Пересчитать баланс"); // 🔄 Новый пункт
                Console.WriteLine("8. Выход");
                Console.Write("Выберите действие: ");

                var choice = Console.ReadLine();
                ICommand command;

                switch (choice)
                {
                    case "1":
                        command = new TimeLoggingCommand(new CreateAccountCommand(_accountService), "Создание счета");
                        break;
                    case "2":
                        command = new TimeLoggingCommand(new ShowAccountsCommand(_accountService), "Просмотр счетов");
                        break;
                    case "3":
                        command = new TimeLoggingCommand(new AddOperationCommand(_operationService, _accountService), "Добавление операции");
                        break;
                    case "4":
                        command = new TimeLoggingCommand(new ShowAnalyticsCommand(_analyticsService, _accountService), "Аналитика");
                        break;
                    case "5":
                        command = new TimeLoggingCommand(new ExportDataCommand(_accountService, _categoryService, _operationService), "Экспорт данных");
                        break;
                    case "6":
                        command = new TimeLoggingCommand(new ImportDataCommand(_accountService, _categoryService, _operationService), "Импорт данных");
                        break;
                    case "7":
                        command = new TimeLoggingCommand(new RecalculateBalanceCommand(_analyticsService, _accountService), "Пересчет баланса");
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод!");
                        continue;
                }

                command.Execute();
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
            }
        }
    }
}

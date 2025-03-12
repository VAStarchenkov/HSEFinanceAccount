using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public class AddOperationCommand : ICommand
    {
        private readonly IOperationService _operationService;
        private readonly IBankAccountService _accountService;

        public AddOperationCommand(IOperationService operationService, IBankAccountService accountService)
        {
            _operationService = operationService;
            _accountService = accountService;
        }

        public void Execute()
        {
            var accounts = _accountService.GetAllAccounts().ToList();
            if (!accounts.Any())
            {
                Console.WriteLine("Нет доступных счетов. Создайте счет сначала.");
                return;
            }

            Console.WriteLine("Выберите счет:");
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {accounts[i].Name} (Баланс: {accounts[i].Balance})");
            }

            if (!int.TryParse(Console.ReadLine(), out int accIndex) || accIndex < 1 || accIndex > accounts.Count)
            {
                Console.WriteLine("Некорректный выбор счета!");
                return;
            }

            var account = accounts[accIndex - 1];

            Console.WriteLine("Выберите тип операции:");
            Console.WriteLine("1. Доход");
            Console.WriteLine("2. Расход");

            if (!int.TryParse(Console.ReadLine(), out int typeChoice) || typeChoice < 1 || typeChoice > 2)
            {
                Console.WriteLine("Некорректный ввод!");
                return;
            }

            OperationType operationType = typeChoice == 1 ? OperationType.Income : OperationType.Expense;

            Console.Write("Введите сумму: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Некорректная сумма!");
                return;
            }

            var operation = _operationService.CreateOperation(operationType, account.Id, amount, DateTime.Now, Guid.Empty);
            Console.WriteLine($"Операция добавлена: {operationType} на сумму {amount}");
        }
    }
}

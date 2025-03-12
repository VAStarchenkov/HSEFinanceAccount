namespace FinanceAccounting.Services
{
    public class CreateAccountCommand : ICommand
    {
        private readonly IBankAccountService _accountService;

        public CreateAccountCommand(IBankAccountService accountService)
        {
            _accountService = accountService;
        }

        public void Execute()
        {
            Console.Write("Введите название счета: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Введите начальный баланс: ");
            if (!decimal.TryParse(Console.ReadLine(), out var balance))
            {
                Console.WriteLine("Ошибка ввода суммы!");
                return;
            }

            var account = _accountService.CreateAccount(name, balance);
            Console.WriteLine($"Счет '{account.Name}' создан с балансом {account.Balance}.");
        }
    }
}

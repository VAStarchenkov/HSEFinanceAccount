using System.Diagnostics;

namespace FinanceAccounting.Services
{
    public class TimeLoggingCommand : ICommand
    {
        private readonly ICommand _command;
        private readonly string _commandName;

        public TimeLoggingCommand(ICommand command, string commandName)
        {
            _command = command;
            _commandName = commandName;
        }

        public void Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            _command.Execute();
            stopwatch.Stop();

            Console.WriteLine($"[{_commandName}] Выполнено за {stopwatch.ElapsedMilliseconds} мс");
        }
    }
}

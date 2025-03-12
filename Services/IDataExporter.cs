using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public interface IDataExporter
    {
        void ExportData(string filePath, IEnumerable<BankAccount> accounts, IEnumerable<Category> categories, IEnumerable<Operation> operations);
        (List<BankAccount>, List<Category>, List<Operation>) ImportData(string filePath);
    }
}

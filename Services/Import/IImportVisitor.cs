using FinanceAccounting.Domain;

namespace FinanceAccounting.Services.Import
{
    public interface IImportVisitor
    {
        (List<BankAccount>, List<Category>, List<Operation>) ImportFromFile(string filePath);
    }
}

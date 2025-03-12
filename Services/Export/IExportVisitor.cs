using FinanceAccounting.Domain;

namespace FinanceAccounting.Services.Export
{
    public interface IExportVisitor
    {
        void Visit(BankAccount account);
        void Visit(Category category);
        void Visit(Operation operation);
        void ExportToFile(string filePath);
    }
}

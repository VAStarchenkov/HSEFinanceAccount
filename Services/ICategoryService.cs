using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public interface ICategoryService
    {
        Category CreateCategory(string name, CategoryType type);
        Category? GetCategoryById(Guid id);
        void DeleteCategory(Guid id);
        IEnumerable<Category> GetAllCategories();
    }
}

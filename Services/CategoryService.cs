using FinanceAccounting.Domain;

namespace FinanceAccounting.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly List<Category> _categories = new();

        public Category CreateCategory(string name, CategoryType type)
        {
            var category = new Category(name, type);
            _categories.Add(category);
            return category;
        }

        public Category? GetCategoryById(Guid id) =>
            _categories.FirstOrDefault(c => c.Id == id);

        public void DeleteCategory(Guid id) =>
            _categories.RemoveAll(c => c.Id == id);

        public IEnumerable<Category> GetAllCategories() => _categories;
    }
}

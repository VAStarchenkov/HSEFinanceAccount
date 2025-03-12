using FinanceAccounting.Domain;
using System.Collections.Concurrent;

namespace FinanceAccounting.Services.Proxy
{
    public class CategoryProxy : ICategoryService, ICacheProxy<Category>
    {
        private readonly ICategoryService _realService;
        private readonly ConcurrentDictionary<Guid, Category> _cache = new();

        public CategoryProxy(ICategoryService realService)
        {
            _realService = realService;
        }

        public Category CreateCategory(string name, CategoryType type)
        {
            var category = _realService.CreateCategory(name, type);
            _cache[category.Id] = category;
            return category;
        }

        public Category? GetCategoryById(Guid id)
        {
            return _cache.TryGetValue(id, out var category) ? category : _realService.GetCategoryById(id);
        }

        public void DeleteCategory(Guid id)
        {
            _realService.DeleteCategory(id);
            _cache.TryRemove(id, out _);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            if (_cache.Count == 0)
            {
                var categories = _realService.GetAllCategories();
                foreach (var cat in categories)
                {
                    _cache[cat.Id] = cat;
                }
            }
            return _cache.Values;
        }

        public IEnumerable<Category> GetAll() => GetAllCategories();
        public void Add(Category item) => _cache[item.Id] = item;
        public void ClearCache() => _cache.Clear();
    }
}

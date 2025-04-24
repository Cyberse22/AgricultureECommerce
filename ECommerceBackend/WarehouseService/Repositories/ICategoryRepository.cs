using WarehouseService.Data;

namespace WarehouseService.Repositories;

public interface ICategoryRepository
{
    Task<Category> GetCategoryByIdAsync(string categoryId);
    Task<Category> GetCategoryByNameAsync(string categoryName);
    Task <List<Category>> GetCategoryByCategoryParentAsync(string categoryParent);
    Task<List<Category>> GetAllCategoriesAsync();
    Task CreateCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
}
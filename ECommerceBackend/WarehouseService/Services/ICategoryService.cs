using WarehouseService.Data;
using WarehouseService.Models;

namespace WarehouseService.Services;

public interface ICategoryService
{
    Task<CategoryModel> GetCategoryByIdAsync(string categoryId);
    Task<CategoryModel> GetCategoryByNameAsync(string categoryName);
    Task<List<CategoryModel>> GetCategoryByCategoryParentAsync(string categoryParent);
    Task<List<CategoryModel>> GetAllCategoriesAsync();
    Task<UpdateCategoryModel> CreateCategoryAsync(UpdateCategoryModel model);
    Task<UpdateCategoryModel> UpdateCategoryAsync(UpdateCategoryModel model);
    Task<string> UploadCategoryImageAsync(UploadCategoryImage model);
}
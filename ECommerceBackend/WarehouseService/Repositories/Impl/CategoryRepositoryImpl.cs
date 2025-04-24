using MongoDB.Driver;
using WarehouseService.Data;
using WarehouseService.Data.MongoDB;

namespace WarehouseService.Repositories.Impl;

public class CategoryRepositoryImpl : ICategoryRepository
{
    private readonly IMongoCollection<Category> _categories;

    public CategoryRepositoryImpl(MongoDbServices mongoDbServices)
    {
        _categories = mongoDbServices.Database.GetCollection<Category>("categorycollection");
    }

    public async Task CreateCategoryAsync(Category category) => await _categories.InsertOneAsync(category);

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _categories.Find(_ => true).ToListAsync();
    }

    public async Task<List<Category>> GetCategoryByCategoryParentAsync(string categoryParent)
    {
        return await _categories.Find(c => c.CategoryParent == categoryParent).ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(string categoryId)
    {
        return await _categories.Find(c => c.CategoryId == categoryId).FirstOrDefaultAsync();
    }

    public async Task<Category> GetCategoryByNameAsync(string categoryName)
    {
        return await _categories.Find(c => c.CategoryName == categoryName).FirstOrDefaultAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        var update = Builders<Category>.Update
                .Set(c => c.CategoryName, category.CategoryName)
                .Set(c => c.CategoryDescription, category.CategoryDescription)
                .Set(c => c.CategoryParent, category.CategoryParent);

        var result = await _categories.UpdateOneAsync(x => x.CategoryId == category.CategoryId, update);

        if (result.MatchedCount == 0)
        {
            throw new Exception("Product not found.");
        }
    }
}
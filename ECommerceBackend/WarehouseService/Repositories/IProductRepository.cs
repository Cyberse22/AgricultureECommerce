using WarehouseService.Data;

namespace WarehouseService.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(string productId);
        Task <List<Product>> GetProductByKeywordAsync(string keyword);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByCategoryIdAsync(string categoryId);
        Task<List<Product>> GetProductsByInventoryIdAsync(string inventoryId);
        Task CreateProductAsync (Product product);
        Task UpdateProductAsync (Product product);
        Task UpdateProductQuantityAsync(Product product);

        Task UploadImageAsync(string productId, List<string> imageUrl);

    }
}

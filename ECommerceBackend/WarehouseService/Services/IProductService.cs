using WarehouseService.Models;

namespace WarehouseService.Services
{
    public interface IProductService
    {
        Task<List<GetProductModel>> GetProducts();
        Task<List<GetProductModel>> SearchProductByKeyword(string keyword);
        Task<GetProductModel> GetProductById(string productId);
        Task<GetProductModel> GetProductByCategoryId(string categoryId);
        Task<GetProductModel> GetProductByInventoryId(string inventoryId);
        Task<List<IFormFile>> UpdateImage(UpdateImageModel model);
        Task<CreateProductModel> CreateProductAsync(CreateProductModel model);
        Task<UpdateProductModel> UpdateProductAsync(string productId, UpdateProductModel model);
    }
}

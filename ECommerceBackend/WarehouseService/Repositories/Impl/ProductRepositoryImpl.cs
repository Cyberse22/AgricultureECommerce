using MongoDB.Driver;
using WarehouseService.Data;
using WarehouseService.Data.MongoDB;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace WarehouseService.Repositories.Impl
{
    public class ProductRepositoryImpl : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepositoryImpl(MongoDbServices? mongoDbService)
        {
            _products = mongoDbService!.Database!.GetCollection<Product>("productcollection");
        }

        public async Task CreateProductAsync(Product product) => await _products.InsertOneAsync(product);

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            return await _products.Find(x => x.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProductByKeywordAsync(string keyword)
        {
            return await _products
                .Find(p => p.ProductName.ToLower().Contains(keyword.ToLower()))
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(string categoryId)
        {
            return await _products.Find(x => x.CategoryIds!.Contains(categoryId)).ToListAsync();
        }

        public async Task<List<Product>> GetProductsByInventoryIdAsync(string inventoryId)
        {
            return await _products.Find(x => x.InventoryId == inventoryId).ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var update = Builders<Product>.Update
                    .Set(p => p.ProductName, product.ProductName)
                    .Set(p => p.ProductDescription, product.ProductDescription)
                    .Set(p => p.ProductPrice, product.ProductPrice);
                    

            var result = await _products.UpdateOneAsync(x => x.ProductId == product.ProductId, update);

            if (result.MatchedCount == 0)
            {
                throw new Exception("Product not found.");
            }
        }

        public async Task UpdateProductQuantityAsync(Product product)
        {
            var update = Builders<Product>.Update
                    .Set(p => p.Quantity, product.Quantity)
                    .Set(p => p.AvailableQuantity, product.AvailableQuantity);


            var result = await _products.UpdateOneAsync(x => x.ProductId == product.ProductId, update);

            if (result.MatchedCount == 0)
            {
                throw new Exception("Product not found.");
            }
        }

        public async Task UploadImageAsync(string productId, List<string> imageUrl)
        {
            var update = Builders<Product>.Update.Set(p => p.ProductImage, imageUrl);
            var result = await _products.UpdateOneAsync(x => x.ProductId == productId, update);
            if (result.MatchedCount == 0)
            {
                throw new Exception("Product not found.");
            }
        }
    }
}

namespace WarehouseService.Data.MongoDB
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ProductServiceCollectionName { get; set; } = string.Empty;
        public string CategoryServiceCollectionName { get; set; } = string.Empty;
        public string ProdCateServiceCollectionName { get; set; } = string.Empty;
        public string InventoryServiceCollectionName { get; set; } = string.Empty;
        public string TransactionServiceCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }

    public interface IMongoDbSettings
    {
        string ProductServiceCollectionName { get; set; }
        string CategoryServiceCollectionName { get; set; }
        string ProdCateServiceCollectionName { get; set; }
        string InventoryServiceCollectionName { get; set; }
        string TransactionServiceCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

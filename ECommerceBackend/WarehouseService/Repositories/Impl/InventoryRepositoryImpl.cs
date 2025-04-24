using MongoDB.Driver;
using WarehouseService.Data;
using WarehouseService.Data.MongoDB;

namespace WarehouseService.Repositories.Impl;

public class InventoryRepositoryImpl : IInventoryRepository
{
    private readonly IMongoCollection<Inventory> _inventory;
    private readonly IMongoCollection<InventoryTransaction> _transaction;
    private readonly IMongoCollection<Product> _product;

    public InventoryRepositoryImpl(MongoDbServices mongoDbServices)
    {
        _inventory = mongoDbServices.Database.GetCollection<Inventory>("inventorycollection");
        _transaction = mongoDbServices.Database.GetCollection<InventoryTransaction>("transactioncollection");
        _product = mongoDbServices.Database.GetCollection<Product>("productcollection");
    }

    public async Task CreateInventoryAsync(Inventory inventory) => await _inventory.InsertOneAsync(inventory);
    
    public async Task CreateTransactionAsync(InventoryTransaction transaction) => await _transaction.InsertOneAsync(transaction);

    public async Task<List<Inventory>> GetAllInventoriesAsync()
    {
        return await _inventory.Find(_ => true).ToListAsync();
    }

    public async Task<List<InventoryTransaction>> GetAllTransactionsAsync()
    {
        return await _transaction.Find(_ => true).ToListAsync();
    }

    public async Task<Inventory> GetInvetoryByIdAsync(string inventoryId)
    {
        return await _inventory.Find(x => x.InventoryId == inventoryId).FirstOrDefaultAsync();
    }

    public Task<InventoryTransaction> GetTransactionByIdAsync(string transactionId)
    {
        return _transaction.Find(x => x.TransactionId == transactionId).FirstOrDefaultAsync();
    }

    public async Task UpdateInventoryAsync(Inventory inventory) {
        var update = Builders<Inventory>.Update
                            .Set(i => i.InventoryName, inventory.InventoryName);


        var result = await _inventory.UpdateOneAsync(x => x.InventoryId == inventory.InventoryId, update);

        if (result.MatchedCount == 0)
        {
            throw new Exception("Product not found.");
        }
    }
}
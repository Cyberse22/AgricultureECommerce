using WarehouseService.Data;

namespace WarehouseService.Repositories;

public interface IInventoryRepository
{
    Task <Inventory> GetInvetoryByIdAsync(string inventoryId);
    Task <List<Inventory>> GetAllInventoriesAsync();
    Task CreateInventoryAsync(Inventory inventory);
    Task UpdateInventoryAsync(Inventory inventory);
    Task<List<InventoryTransaction>> GetAllTransactionsAsync();
    Task<InventoryTransaction> GetTransactionByIdAsync(string transactionId);
    Task CreateTransactionAsync(InventoryTransaction transaction);
}
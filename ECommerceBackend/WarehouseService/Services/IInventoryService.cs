using WarehouseService.Models;

namespace WarehouseService.Services;

public interface IInventoryService
{
    Task<InventoryModel> GetInventoryByIdAsync(string inventoryId);
    Task<List<InventoryModel>> GetAllInventoriesAsync();
    Task<InventoryModel> CreateInventoryAsync(InventoryModel model);
    Task<InventoryModel> UpdateInventoryAsync(string inventoryId, InventoryModel model);
    Task<string> ImportTransaction(TransactionModel model);
    Task<string> ExportTransaction(TransactionModel model);
    Task<List<TransactionModel>> GetTransactionsByInventoryIdAsync(string inventoryId);
    Task<TransactionModel> GetTransactionByIdAsync(string transactionId);
}
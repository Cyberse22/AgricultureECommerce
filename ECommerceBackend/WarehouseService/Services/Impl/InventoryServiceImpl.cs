using AutoMapper;
using WarehouseService.Data;
using WarehouseService.Models;
using WarehouseService.Repositories;

namespace WarehouseService.Services.Impl;

public class InventoryServiceImpl : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public InventoryServiceImpl(IInventoryRepository inventoryRepository, IMapper mapper, IProductRepository productRepository)
    {
        _inventoryRepository = inventoryRepository;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<InventoryModel> CreateInventoryAsync(InventoryModel model)
    {
        var inventory = _mapper.Map<Inventory>(model);
        await _inventoryRepository.CreateInventoryAsync(inventory);
        return _mapper.Map<InventoryModel>(inventory);
    }

    public async Task<string> ExportTransaction(TransactionModel model)
    {
        model.Type = "Export";
        var transaction = _mapper.Map<InventoryTransaction>(model);

        foreach (var item in model.Products)
        {
            var product = await _productRepository.GetProductByIdAsync(item.ProductId);
            if (product == null)
            {
                throw new Exception($"Product with ID {item.ProductId} not found.");
            }

            if (product.AvailableQuantity < item.Quantity)
            {
                throw new Exception($"Not enough stock for Product {item.ProductId}.");
            }

            product.Quantity -= item.Quantity;
            product.AvailableQuantity -= item.Quantity;

            await _productRepository.UpdateProductQuantityAsync(product);
        }
        await _inventoryRepository.CreateTransactionAsync(transaction);
        return transaction.TransactionId;
    }

    public async Task<List<InventoryModel>> GetAllInventoriesAsync()
    {
        var inventories = await _inventoryRepository.GetAllInventoriesAsync();
        if (inventories == null || !inventories.Any())
        {
            throw new Exception("No inventories found.");
        }
        return _mapper.Map<List<InventoryModel>>(inventories);
    }

    public async Task<InventoryModel> GetInventoryByIdAsync(string inventoryId)
    {
        var inventory = await _inventoryRepository.GetInvetoryByIdAsync(inventoryId);
        if (inventory == null)
        {
            throw new Exception("Inventory not found.");
        }
        return _mapper.Map<InventoryModel>(inventory);
    }

    public async Task<TransactionModel> GetTransactionByIdAsync(string transactionId)
    {
        var transaction = await _inventoryRepository.GetTransactionByIdAsync(transactionId);
        if (transaction == null)
        {
            throw new Exception("Transaction not found.");
        }

        var result = _mapper.Map<TransactionModel>(transaction);

        foreach (var item in result.Products)
        {
            var product = await _productRepository.GetProductByIdAsync(item.ProductId);
            item.ProductName = product?.ProductName ?? "Unknown";
        }

        return result;
    }

    public async Task<List<TransactionModel>> GetTransactionsByInventoryIdAsync(string inventoryId)
    {
        var inventory = await _inventoryRepository.GetInvetoryByIdAsync(inventoryId);
        if (inventory == null)
        {
            throw new Exception("Inventory not found.");
        }
        var transactions = await _inventoryRepository.GetAllTransactionsAsync();
        if (transactions == null || !transactions.Any())
        {
            throw new Exception("No transactions found for the given inventory ID.");
        }
        var result = transactions.Where(t => t.InventoryId == inventoryId).ToList();
        return _mapper.Map<List<TransactionModel>>(result);
    }

    public async Task<string> ImportTransaction(TransactionModel model)
    {
        model.Type = "Import";
        var transaction = _mapper.Map<InventoryTransaction>(model);

        foreach (var item in model.Products)
        {
            var product = await _productRepository.GetProductByIdAsync(item.ProductId);
            if (product == null)
            {
                throw new Exception($"Product with ID {item.ProductId} not found.");
            }

            product.Quantity += item.Quantity;
            product.AvailableQuantity += item.Quantity;

            await _productRepository.UpdateProductQuantityAsync(product);
        }
        await _inventoryRepository.CreateTransactionAsync(transaction);
        return transaction.TransactionId;
    }

    public async Task<InventoryModel> UpdateInventoryAsync(string inventoryId, InventoryModel model)
    {
        var inventory = await _inventoryRepository.GetInvetoryByIdAsync(inventoryId);
        if (inventory == null)
        {
            throw new Exception("Inventory not found.");
        }
        var updatedInventory = _mapper.Map<Inventory>(model);
        await _inventoryRepository.UpdateInventoryAsync(updatedInventory);
        return _mapper.Map<InventoryModel>(updatedInventory);
    }
}
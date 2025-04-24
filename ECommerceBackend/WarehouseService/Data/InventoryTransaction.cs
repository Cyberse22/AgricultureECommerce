using MongoDB.Bson.Serialization.Attributes;
using WarehouseService.Models;

namespace WarehouseService.Data;

public class InventoryTransaction : BaseEntity
{
    [BsonElement]
    public string? TransactionId { get; set; }
    [BsonElement]
    public string InventoryId { get; set; }
    [BsonElement]
    public DateTime TransactionDate { get; set; }
    [BsonElement]
    public string Type { get; set; } // Import or Export
    [BsonElement]
    public List<ProductTransaction> Products { get; set; } 
}

public class ProductTransaction
{
    [BsonElement]
    public string ProductId { get; set; }
    [BsonElement]
    public string ProductName { get; set; }
    [BsonElement]
    public int Quantity { get; set; }
}